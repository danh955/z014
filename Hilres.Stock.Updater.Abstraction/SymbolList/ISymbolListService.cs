// <copyright file="ISymbolListService.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater.Abstraction.SymbolList
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Symbol list service interface.
    /// </summary>
    public interface ISymbolListService
    {
        /// <summary>
        /// Get all the symbols.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>NasdaqSymbolsResult.</returns>
        public Task<ISymbolListResult> GetSymbleList(CancellationToken cancellationToken);
    }
}