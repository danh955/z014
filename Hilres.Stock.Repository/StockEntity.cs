// <copyright file="StockEntity.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// Stock class.
    /// </summary>
    public class StockEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockEntity"/> class.
        /// </summary>
        public StockEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockEntity"/> class.
        /// </summary>
        /// <param name="symbol">Stock symbol.</param>
        /// <param name="stockPrices">List of stock prices.</param>
        public StockEntity(string symbol, List<StockPriceEntity> stockPrices)
        {
            this.Symbol = symbol;
            this.StockPrices = stockPrices;
        }

        /// <summary>
        /// Gets or sets the stock ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets the stock symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets list of stock prices.
        /// </summary>
        public List<StockPriceEntity> StockPrices { get; set; }
    }
}