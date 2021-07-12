// <copyright file="DownloadYahooFinanceServiceTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance.Tests
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Download.Abstraction;
    using Hilres.Stock.Download.YahooFinance.Tests.Helper;
    using Xunit;

    /// <summary>
    /// Download Yahoo Finance service test class.
    /// </summary>
    public class DownloadYahooFinanceServiceTest
    {
        /// <summary>
        /// Handler test.
        /// </summary>
        /// <param name="symbol">Stock symbol.</param>
        /// <param name="firstDate">The first date.</param>
        /// <param name="lastDate">The last date.</param>
        /// <param name="interval">Interval of the data.</param>
        /// <param name="count">Expected number of rows returned.</param>
        /// <returns>Task.</returns>
        [Theory]
        [InlineData("MSFT", "3/9/2021", "3/14/2021", StockInterval.Daily, 4)]
        [InlineData("MSFT", "12/14/2020", "3/14/2021", StockInterval.Weekly, 14)]
        [InlineData("MSFT", "10/1/2021", "3/14/2021", StockInterval.Monthly, 7)]
        public async Task GetStockPricesAsyncTest(string symbol, string firstDate, string lastDate, StockInterval interval, int count)
        {
            var service = new DownloadYahooFinanceService(YahooPriceHistoryHelper.MockIHttpClientFactory());
            var result = await service.GetStockPricesAsync(symbol, DateTime.Parse(firstDate), DateTime.Parse(lastDate), interval, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.Prices);
            Assert.True(result.Prices.Any());
            Assert.Equal(count, result.Prices.Count());
        }
    }
}