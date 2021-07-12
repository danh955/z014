// <copyright file="SymbolListResult.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.Abstraction
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Symbol list result interface.
    /// </summary>
    /// <param name="Symbols">List of symbols.</param>
    /// <param name="FileCreationTime">Symbol file creation time.</param>
    public record SymbolListResult(IEnumerable<SymbolListItem> Symbols, DateTime FileCreationTime);
}