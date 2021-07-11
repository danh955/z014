// <copyright file="PriceListResult.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.Abstraction
{
    using System.Collections.Generic;

    /// <summary>
    /// List of stock price result interface.
    /// </summary>
    /// <param name="Prices">List of stock prices.</param>
    /// <param name="ErrorMessage">Error message.  Null if successful.</param>
    public record PriceListResult(IEnumerable<PriceListItem> Prices, string ErrorMessage = null);
}