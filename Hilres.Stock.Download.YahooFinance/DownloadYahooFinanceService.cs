// <copyright file="DownloadYahooFinanceService.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance
{
    using System.Net.Http;
    using Hilres.Stock.Download.Abstraction;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Download Yahoo Finance service class.
    /// </summary>
    public partial class DownloadYahooFinanceService : IStockDownloadService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<DownloadYahooFinanceService> logger;
        private readonly Token token;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadYahooFinanceService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">IHttpClientFactory.</param>
        /// <param name="logger">ILogger.</param>
        public DownloadYahooFinanceService(IHttpClientFactory httpClientFactory, ILogger<DownloadYahooFinanceService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpClient = httpClientFactory.CreateClient();
            this.logger = logger;
            this.token = new Token(logger);
        }
    }
}