// <copyright file="DownloadYahooFinanceService.SymbolList.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Download.Abstraction;

    /// <summary>
    /// Download Yahoo Finance service class for the symbol list.
    /// </summary>
    public partial class DownloadYahooFinanceService
    {
        /// <inheritdoc/>
        public Task<SymbolListResult> GetAllSymbolsAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}