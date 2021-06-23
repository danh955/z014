// <copyright file="StockEntity.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Repository
{
    using System.Collections.Generic;
    using LiteDB;

    /// <summary>
    /// Stock class.
    /// </summary>
    public class StockEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockEntity"/> class.
        /// </summary>
        /// <param name="id">Object id.</param>
        /// <param name="symbol">Stock symbol.</param>
        [BsonCtor]
        public StockEntity(ObjectId id, string symbol)
        {
            this.Id = id;
            this.Symbol = symbol;
        }

        /// <summary>
        /// Gets or sets the stock ID.
        /// </summary>
        public ObjectId Id { get; set; }

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