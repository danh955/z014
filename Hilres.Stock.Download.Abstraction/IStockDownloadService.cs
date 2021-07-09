// <copyright file="IStockDownloadService.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.Abstraction
{
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
        public Task<ISymbolListResult> GetSymbleList(CancellationToken cancellationToken);
    }
}