// <copyright file="SymbolListItem.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.NasdaqTrader
{
    using Hilres.Stock.Download.Abstraction;

    /// <summary>
    /// An item in the symbol list.
    /// </summary>
    internal class SymbolListItem : ISymbolListItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolListItem"/> class.
        /// </summary>
        /// <param name="symbol">Identifier for the security.</param>
        /// <param name="securityName">Company issuing the security.</param>
        /// <param name="exchange">Listing stock exchange or market of the security.</param>
        internal SymbolListItem(string symbol, string securityName, string exchange)
        {
            this.Symbol = symbol;
            this.SecurityName = securityName;
            this.Exchange = exchange;
        }

        /// <summary>
        /// Gets the identifier for the security.
        /// </summary>
        public string Symbol { get; init; }

        /// <summary>
        /// Gets company issuing the security.
        /// </summary>
        public string SecurityName { get; init; }

        /// <summary>
        /// Gets the listing stock exchange or market of the security.
        /// </summary>
        public string Exchange { get; init; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Symbol}, {this.Exchange}, {this.SecurityName}";
        }
    }
}