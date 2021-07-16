// <copyright file="DownloadYahooFinanceService.SymbolList.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Download.Abstraction;
    using Hilres.Stock.Download.NasdaqTrader;

    /// <summary>
    /// Download Yahoo Finance service class for the symbol list.
    /// </summary>
    public partial class DownloadYahooFinanceService
    {
        /// <inheritdoc/>
        public async Task<SymbolListResult> GetAllSymbolsAsync(CancellationToken cancellationToken)
        {
            var service = new DownloadNasdaqTraderService(this.httpClientFactory);
            var result = await service.GetAllSymbolsAsync(cancellationToken);

            // Return symbols that only has alphanumeric characters.
            return new SymbolListResult(
                            Symbols: result.Symbols.Where(s => s.Symbol.All(char.IsLetter)),
                            FileCreationTime: result.FileCreationTime);
        }
    }
}