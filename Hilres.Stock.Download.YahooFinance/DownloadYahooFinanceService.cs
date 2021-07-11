// <copyright file="DownloadYahooFinanceService.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance
{
    using System.Net.Http;
    using Hilres.Stock.Download.Abstraction;

    /// <summary>
    /// Download Yahoo Finance service class.
    /// </summary>
    public partial class DownloadYahooFinanceService : IStockDownloadService
    {
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadYahooFinanceService"/> class.
        /// </summary>
        /// <param name="httpClientFactory">IHttpClientFactory.</param>
        public DownloadYahooFinanceService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
    }
}