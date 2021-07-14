// <copyright file="IStockDownloadService.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.Abstraction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Stock download service interface.
    /// </summary>
    public interface IStockDownloadService
    {
        /// <summary>
        /// Get all the symbols.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>NasdaqSymbolsResult.</returns>
        public Task<SymbolListResult> GetAllSymbolsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get stock history data from Yahoo.
        /// </summary>
        /// <param name="symbol">Symbol of prices to get.</param>
        /// <param name="firstDate">First date.</param>
        /// <param name="lastDate">Last date.</param>
        /// <param name="interval">Stock price interval.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task with PriceListResult.</returns>
        public Task<PriceListResult> GetStockPricesAsync(string symbol, DateTime firstDate, DateTime lastDate, StockInterval interval, CancellationToken cancellationToken);
    }
}