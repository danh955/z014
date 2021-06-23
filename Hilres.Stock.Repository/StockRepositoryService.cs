// <copyright file="StockRepositoryService.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Repository
{
    using System;
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
        /// <param name="connectionString"> LiteDB connection string.</param>
        public StockRepositoryService(string connectionString)
        {
            this.db = new LiteDatabase(connectionString);

            this.stocks = this.db.GetCollection<StockEntity>("Stock");
            this.stocks.EnsureIndex(s => s.Symbol, unique: true);
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
    }
}