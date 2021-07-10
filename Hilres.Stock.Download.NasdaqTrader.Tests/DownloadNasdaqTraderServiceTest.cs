// <copyright file="DownloadNasdaqTraderServiceTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.NasdaqTrader.Tests
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Download.NasdaqTrader.Tests.Helper;
    using Xunit;

    /// <summary>
    /// Download NASDQ trader service test class.
    /// </summary>
    public class DownloadNasdaqTraderServiceTest
    {
        /// <summary>
        /// Get symbol list test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetSymbolListTestAsync()
        {
            var service = new DownloadNasdaqTraderService(MockData.MockIHttpClientFactoryForNasdaqSymbols());
            var result = await service.GetSymbolListAsync(CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.Symbols);
            Assert.True(result.Symbols.Any());
        }
    }
}