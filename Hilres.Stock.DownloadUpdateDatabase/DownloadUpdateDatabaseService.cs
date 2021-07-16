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
                this.logger.LogInformation("Symbol: {0}", symbol);

                var stockTask = this.downloadService.GetStockPricesAsync(symbol, DateTime.Today.AddYears(-1), DateTime.Today, StockInterval.Daily, cancellationToken);
                var dbStock = this.db.GetStockBySymbol(symbol);
                var downloadedStock = await stockTask;

                if (downloadedStock != null && downloadedStock.Prices != null && downloadedStock.ErrorMessage == null)
                {
                    var newPrices = downloadedStock.Prices.Select(s =>
                                new StockPriceEntity()
                                {
                                    Period = s.Date,
                                    Open = s.Open,
                                    Low = s.Low,
                                    High = s.High,
                                    Close = s.Close,
                                    AdjClose = s.AdjClose,
                                    Volume = s.Volume,
                                })
                                .ToList();

                    if (dbStock == null)
                    {
                        this.db.AddStock(new(symbol, newPrices));
                    }
                    else
                    {
                        dbStock.StockPrices = newPrices;
                        this.db.UpdateStock(dbStock);
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