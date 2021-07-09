// <copyright file="SymbolListResult.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Download.NasdaqTrader
{
    using System;
    using System.Collections.Generic;
    using Hilres.Stock.Download.Abstraction;

    /// <summary>
    /// Result from the NASDAQ symbols query.
    /// </summary>
    internal class SymbolListResult : ISymbolListResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolListResult"/> class.
        /// </summary>
        /// <param name="symbols">List of symbols.</param>
        /// <param name="fileCreationTime">Symbol file creation time.</param>
        internal SymbolListResult(IEnumerable<SymbolListItem> symbols, DateTime fileCreationTime)
        {
            this.Symbols = symbols;
            this.FileCreationTime = fileCreationTime;
        }

        /// <summary>
        /// Gets NASDAQ symbols file creation time.
        /// </summary>
        public DateTime FileCreationTime { get; init; }

        /// <summary>
        /// Gets list of items.
        /// </summary>
        public IEnumerable<ISymbolListItem> Symbols { get; init; }
    }
}