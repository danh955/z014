// <copyright file="SymbolListItem.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.Abstraction
{
    /// <summary>
    /// Symbol list item record.
    /// </summary>
    /// <param name="Symbol">Identifier for the security.</param>
    /// <param name="SecurityName">Company issuing the security.</param>
    /// <param name="Exchange">Listing stock exchange or market of the security.</param>
    public record SymbolListItem(string Symbol, string SecurityName, string Exchange);
}