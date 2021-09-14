// <copyright file="DownloadUpdateDatabaseService.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.DownloadUpdateDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Download.Abstraction;
    using Hilres.Stock.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Download stock data and update database service class.
    /// </summary>
    public class DownloadUpdateDatabaseService
    {
        private readonly StockRepositoryService db;
        private readonly IStockDownloadService downloadService;
        private readonly ILogger<DownloadUpdateDatabaseService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadUpdateDatabaseService"/> class.
        /// </summary>
        /// <param name="downloadService">IStockDownloadService.</param>
        /// <param name="db">StockRepositoryService.</param>
        /// <param name="logger">ILogger.</param>
        public DownloadUpdateDatabaseService(ILogger<DownloadUpdateDatabaseService> logger, IStockDownloadService downloadService, StockRepositoryService db)
        {
            this.downloadService = downloadService;
            this.db = db;
            this.logger = logger;
        }

        /// <summary>
        /// Update the database from the stock download services.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        public async Task UpdataDb(CancellationToken cancellationToken)
        {
            await this.AddNewSymbols(cancellationToken);
            await this.UpdatePrices(cancellationToken);
        }

        private async Task UpdatePrices(CancellationToken cancellationToken)
        {
            var dbSymbols = this.db.GetAllSymbols();

            foreach (var symbol in dbSymbols)
            {
                this.logger.LogDebug("Processing symbol {0}", symbol);
                var downloadedStock = await this.downloadService.GetStockPricesAsync(symbol, DateTime.Today.AddYears(-1), DateTime.Today, StockInterval.Daily, cancellationToken);
                this.logger.LogDebug("Got {0:#,##0} records from download service.", downloadedStock.Prices.Count());
                var dbStock = this.db.GetStockBySymbol(symbol);
                this.logger.LogDebug("Got {0:#,##0} records from database.", dbStock.StockPrices.Count);

                if (downloadedStock != null && downloadedStock.Prices != null && downloadedStock.ErrorMessage == null)
                {
                    var newPrices = downloadedStock.Prices
                        .Where(s => !(s.Open == null && s.Low == null && s.High == null && s.Close == null && s.AdjClose == null && s.Volume == null))
                        .Select(s =>
                                new StockPriceEntity()
                                {
                                    Period = s.Date,
                                    Open = s.Open ?? 0,
                                    Low = s.Low ?? 0,
                                    High = s.High ?? 0,
                                    Close = s.Close ?? 0,
                                    AdjClose = s.AdjClose ?? 0,
                                    Volume = s.Volume ?? 0,
                                })
                                .ToList();

                    if (dbStock == null)
                    {
                        this.logger.LogDebug("AddStock");
                        this.db.AddStock(new(symbol, newPrices));
                    }
                    else
                    {
                        if ((!dbStock.StockPrices.Any() && newPrices.Any())
                            || (dbStock.StockPrices.Any() && newPrices.Any()
                                && dbStock.StockPrices.First().Volume != newPrices.First().Volume
                                && dbStock.StockPrices.First().Close != newPrices.First().Close
                                && dbStock.StockPrices.First().AdjClose != newPrices.First().AdjClose))
                        {
                            this.logger.LogDebug("UpdateStock");
                            dbStock.StockPrices = newPrices;
                            this.db.UpdateStock(dbStock);
                        }
                    }
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Add new symbols to database.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        private async Task AddNewSymbols(CancellationToken cancellationToken)
        {
            var symbolResultTask = this.downloadService.GetAllSymbolsAsync(cancellationToken);
            HashSet<string> dbSymbols = new(this.db.GetAllSymbols());
            var symbolResult = await symbolResultTask;

            var newStocks = symbolResult.Symbols
                                .Select(s => s.Symbol.Trim().ToUpper())
                                .Where(symbol => !dbSymbols.Contains(symbol))
                                .Select(symbol => new StockEntity() { Symbol = symbol, StockPrices = new List<StockPriceEntity>() });

            this.db.AddStocks(newStocks);
        }
    }
}