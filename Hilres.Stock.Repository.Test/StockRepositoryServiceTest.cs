// <copyright file="StockRepositoryServiceTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Repository.Test
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    /// <summary>
    /// Stock repository service test class.
    /// </summary>
    public class StockRepositoryServiceTest
    {
        private static readonly List<StockEntity> TestStocks = new()
        {
            new()
            {
                Symbol = "MSFT",
                StockPrices = new()
                {
                    new() { Period = new DateTime(2021, 1, 4), Open = 222.529999, High = 223.000000, Low = 214.809998, Close = 217.690002, AdjOpen = 222.529999, AdjHigh = 223.000000, AdjLow = 214.809998, AdjClose = 216.689423, Volume = 37130100, },
                    new() { Period = new DateTime(2021, 1, 5), Open = 217.259995, High = 218.520004, Low = 215.699997, Close = 217.899994, AdjOpen = 217.259995, AdjHigh = 218.520004, AdjLow = 215.699997, AdjClose = 216.898438, Volume = 23823000, },
                },
            },
            new()
            {
                Symbol = "TSLA",
                StockPrices = new(),
            },
        };

        /// <summary>
        /// Add stock test.
        /// </summary>
        [Fact]
        public void AddStockTest()
        {
            using var db = GetEmptyTestDatabase();
            var testStock = TestStocks[0];

            db.AddStock(testStock);

            var dbStock = db.GetStockBySymbol(testStock.Symbol);
            Assert.NotNull(dbStock);
        }

        /// <summary>
        /// Get stock by symbol test.
        /// </summary>
        [Fact]
        public void GetStockBySymbolTest()
        {
            using var db = GetTestDatabaseWithData();
            var testStock = TestStocks[0];

            var msftStock = db.GetStockBySymbol(testStock.Symbol.ToLower());
            Assert.NotNull(msftStock);
            Assert.True(msftStock.StockPrices.Count == testStock.StockPrices.Count, $"Count must be {testStock.StockPrices.Count}");
        }

        /// <summary>
        /// Get an empty memory database.
        /// </summary>
        /// <returns>StockRepositoryService.</returns>
        private static StockRepositoryService GetEmptyTestDatabase()
        {
            using var db = new StockRepositoryService(":memory:");
            return db;
        }

        /// <summary>
        /// Get an in memory database with test data.
        /// </summary>
        /// <returns>StockRepositoryService.</returns>
        private static StockRepositoryService GetTestDatabaseWithData()
        {
            using var db = GetEmptyTestDatabase();
            db.AddStock(TestStocks);
            return db;
        }
    }

    /*
        Date,Open,High,Low,Close,Adj Close,Volume
        2021-01-04,222.529999,223.000000,214.809998,217.690002,216.689423,37130100
        2021-01-05,217.259995,218.520004,215.699997,217.899994,216.898438,23823000
        2021-01-06,212.169998,216.490005,211.940002,212.250000,211.274414,35930700
        2021-01-07,214.039993,219.339996,213.710007,218.289993,217.286652,27694500
        2021-01-08,218.679993,220.580002,217.029999,219.619995,218.610550,22956200
    */
}