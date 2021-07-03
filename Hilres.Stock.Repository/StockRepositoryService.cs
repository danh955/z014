// <copyright file="StockRepositoryService.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using LiteDB;

    /// <summary>
    /// Stock repository service class.
    /// </summary>
    public class StockRepositoryService : IDisposable
    {
        private readonly LiteDatabase db;
        private readonly ILiteCollection<StockEntity> stocks;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockRepositoryService"/> class.
        /// </summary>
        /// <param name="connectionString"> LiteDB connection string.  Use ":memory:" for an in memory database.</param>
        public StockRepositoryService(string connectionString)
        {
            if (connectionString == ":memory:")
            {
                var memoryStream = new MemoryStream();
                this.db = new LiteDatabase(memoryStream);
            }
            else
            {
                this.db = new LiteDatabase(connectionString);
            }

            this.stocks = this.db.GetCollection<StockEntity>("Stock");
            this.stocks.EnsureIndex(s => s.Symbol, unique: true);
        }

        /// <summary>
        /// Add a stock entity into the database.
        /// </summary>
        /// <param name="stock">StockEntity to add.</param>
        public void AddStock(StockEntity stock)
        {
            stock.Symbol = stock.Symbol.Trim().ToUpper();
            this.stocks.Insert(stock);
        }

        /// <summary>
        /// Add a list of stock entities into the database.
        /// </summary>
        /// <param name="stocks">StockEntity to add.</param>
        public void AddStock(IEnumerable<StockEntity> stocks)
        {
            foreach (var stock in stocks)
            {
                stock.Symbol = stock.Symbol.Trim().ToUpper();
            }

            this.stocks.Insert(stocks);
        }

        /// <summary>
        /// Delete a stock by its ID.
        /// </summary>
        /// <param name="id">ID of stock.</param>
        public void DeleteStock(int id)
        {
            this.stocks.Delete(id);
        }

        /// <summary>
        /// Delete a stock by its symbol.
        /// </summary>
        /// <param name="symbol">Symbol of stock</param>
        public void DeleteStock(string symbol)
        {
            this.stocks.DeleteMany(item => item.Symbol == symbol);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (this.db != null)
            {
                this.db.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Get a stock by its symbol.
        /// </summary>
        /// <param name="symbol">Symbol of stock.</param>
        /// <returns>StockEntity.</returns>
        public StockEntity GetStockBySymbol(string symbol)
        {
            return this.stocks.FindOne(s => s.Symbol.Contains(symbol));
        }

        /// <summary>
        /// Update a stock.
        /// </summary>
        /// <param name="stock">Stock entity.</param>
        public void UpdateStock(StockEntity stock)
        {
            this.stocks.Update(stock);
        }
    }
}