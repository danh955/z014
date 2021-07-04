// <copyright file="ISymbolListResult.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater.Abstraction.SymbolList
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Symbol list result interface.
    /// </summary>
    public interface ISymbolListResult
    {
        /// <summary>
        /// Gets NASDAQ symbols file creation time.
        /// </summary>
        public DateTime FileCreationTime { get; }

        /// <summary>
        /// Gets list of items.
        /// </summary>
        public IEnumerable<ISymbolListItem> Symbols { get; }
    }
}