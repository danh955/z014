// <copyright file="ISymbolListItem.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Updater.Abstraction.SymbolList
{
    /// <summary>
    /// Symbol list item interface.
    /// </summary>
    public interface ISymbolListItem
    {
        /// <summary>
        /// Gets the identifier for the security.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Gets company issuing the security.
        /// </summary>
        public string SecurityName { get; }

        /// <summary>
        /// Gets the listing stock exchange or market of the security.
        /// </summary>
        public string Exchange { get; }
    }
}