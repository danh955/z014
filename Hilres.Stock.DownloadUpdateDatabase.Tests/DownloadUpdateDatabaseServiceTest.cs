// <copyright file="DownloadUpdateDatabaseServiceTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.DownloadUpdateDatabase.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.DownloadUpdateDatabase.Tests.Helper;
    using Hilres.Stock.Repository;
    using Xunit;

    /// <summary>
    /// Download stock data and update database service test class.
    /// </summary>
    public class DownloadUpdateDatabaseServiceTest
    {
        /// <summary>
        /// Update the database from the stock download services test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task UpdataDbTest()
        {
            var servcie = new DownloadUpdateDatabaseService(new MockStockDownloadService(), GetEmptyTestDatabase());
            await servcie.UpdataDb(CancellationToken.None);
        }

        /// <summary>
        /// Get an empty memory database.
        /// </summary>
        /// <returns>StockRepositoryService.</returns>
        private static StockRepositoryService GetEmptyTestDatabase()
        {
            return new StockRepositoryService(":memory:");
        }
    }
}