// <copyright file="Token.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Fetching token (cookie and crumb) from Yahoo Finance.
    /// </summary>
    public class Token
    {
        private readonly ILogger logger;

        private readonly Regex regexCrumb = new(
                                            "CrumbStore\":{\"crumb\":\"(?<crumb>.+?)\"}",
                                            RegexOptions.CultureInvariant | RegexOptions.Compiled);

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        public Token(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets cookie.
        /// </summary>
        public string Cookie { get; private set; }

        /// <summary>
        /// Gets crumb.
        /// </summary>
        public string Crumb { get; private set; }

        /// <summary>
        /// Refresh cookie and crumb value.
        /// </summary>
        /// <param name="symbol">Stock ticker symbol.</param>
        /// <param name="httpClient">HttpClient.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>True if successful.</returns>
        public async Task RefreshAsync(string symbol, HttpClient httpClient, CancellationToken cancellationToken)
        {
            int tryCount = 4;
            bool successful = true;
            while ((string.IsNullOrWhiteSpace(this.Cookie) || string.IsNullOrWhiteSpace(this.Crumb) || !successful)
                && !cancellationToken.IsCancellationRequested && tryCount > 0)
            {
                successful = await this.RefreshTokenAsync(symbol);
                if (successful)
                {
                    httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Cookie.ToString(), this.Cookie);
                }

                await Task.Delay(1000, cancellationToken);
                tryCount--;
            }
        }

        /// <summary>
        /// Clear token then refresh cookie and crumb value.
        /// </summary>
        /// <param name="symbol">Stock ticker symbol.</param>
        /// <param name="httpClient">HttpClient.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>True if successful.</returns>
        public async Task ClearAndRefreshAsync(string symbol, HttpClient httpClient, CancellationToken cancellationToken)
        {
            this.logger.LogDebug(" = = = = = = ClearAndRefreshAsync = = = = = =");
            this.Cookie = null;
            this.Crumb = null;
            await this.RefreshAsync(symbol, httpClient, cancellationToken);
        }

        /// <summary>
        /// Refresh cookie and crumb value.
        /// </summary>
        /// <param name="symbol">Stock ticker symbol.</param>
        /// <returns>True if successful.</returns>
        private async Task<bool> RefreshTokenAsync(string symbol)
        {
            const string urlScrape = "https://finance.yahoo.com/quote/{0}?p={0}";

            this.logger.LogDebug("RefreshTokenAsync");

            try
            {
                this.Cookie = string.Empty;
                this.Crumb = string.Empty;

                var url = string.Format(urlScrape, symbol);

                var request = (HttpWebRequest)WebRequest.Create(url);

                request.CookieContainer = new CookieContainer();
                request.Method = "GET";

                using var response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
                var cookie = response.GetResponseHeader("Set-Cookie").Split(';')[0];

                var html = string.Empty;

                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        html = await new StreamReader(stream).ReadToEndAsync().ConfigureAwait(false);
                    }
                }

                if (html.Length < 5000)
                {
                    return false;
                }

                var crumb = this.GetCrumb(html);

                if (crumb != null)
                {
                    this.Cookie = cookie;
                    this.Crumb = crumb;
                    this.logger.LogInformation("Crumb: '{0}', Cookie: '{1}'", crumb, cookie);
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Get crumb value from HTML.
        /// </summary>
        /// <param name="html">HTML code.</param>
        /// <returns>Crumb.</returns>
        private string GetCrumb(string html)
        {
            string crumb = null;

            try
            {
                var matches = this.regexCrumb.Matches(html);

                if (matches.Count > 0)
                {
                    crumb = matches[0].Groups["crumb"].Value;

                    // fixed Unicode character 'SOLIDUS'
                    if (crumb.Length != 11)
                    {
                        crumb = crumb.Replace("\\u002F", "/");
                    }
                }
                else
                {
                    this.logger.LogWarning("RegEx no match");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex.Message);
            }

            return crumb;
        }
    }
}