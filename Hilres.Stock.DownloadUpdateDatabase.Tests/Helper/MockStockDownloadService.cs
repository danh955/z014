// <copyright file="MockStockDownloadService.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.DownloadUpdateDatabase.Tests.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Download.Abstraction;

    /// <summary>
    /// Mock stock download service class.
    /// </summary>
    public class MockStockDownloadService : IStockDownloadService
    {
        /// <inheritdoc/>
        public Task<PriceListResult> GetStockPricesAsync(string symbol, DateTime firstDate, DateTime lastDate, StockInterval interval, CancellationToken cancellationToken)
        {
            var prices = symbol switch
            {
                "AAPL" => new List<PriceListItem>()
                                {
                                    new(new(2021, 01, 04), 133.520004, 133.610001, 126.760002, 129.410004, 128.997803, 143301900),
                                    new(new(2021, 01, 05), 128.889999, 131.740005, 128.429993, 131.009995, 130.592697, 97664900),
                                    new(new(2021, 01, 06), 127.720001, 131.050003, 126.379997, 126.599998, 126.196747, 155088000),
                                    new(new(2021, 01, 07), 128.360001, 131.630005, 127.860001, 130.919998, 130.502991, 109578200),
                                    new(new(2021, 01, 08), 132.429993, 132.630005, 130.229996, 132.050003, 131.629379, 105158200),
                                },
                "MSFT" => new List<PriceListItem>()
                                {
                                    new(new(2021, 01, 04), 222.529999, 223.000000, 214.809998, 217.690002, 216.689423, 37130100),
                                    new(new(2021, 01, 05), 217.259995, 218.520004, 215.699997, 217.899994, 216.898438, 23823000),
                                    new(new(2021, 01, 06), 212.169998, 216.490005, 211.940002, 212.250000, 211.274414, 35930700),
                                    new(new(2021, 01, 07), 214.039993, 219.339996, 213.710007, 218.289993, 217.286652, 27694500),
                                    new(new(2021, 01, 08), 218.679993, 220.580002, 217.029999, 219.619995, 218.610550, 22956200),
                                },
                "AMZN" => new List<PriceListItem>()
                                {
                                    new(new(2021, 01, 04), 3270.000000, 3272.000000, 3144.020020, 3186.629883, 3186.629883, 4411400),
                                    new(new(2021, 01, 05), 3166.010010, 3223.379883, 3165.060059, 3218.510010, 3218.510010, 2655500),
                                    new(new(2021, 01, 06), 3146.479980, 3197.510010, 3131.159912, 3138.379883, 3138.379883, 4394800),
                                    new(new(2021, 01, 07), 3157.000000, 3208.540039, 3155.000000, 3162.159912, 3162.159912, 3514500),
                                    new(new(2021, 01, 08), 3180.000000, 3190.639893, 3142.199951, 3182.699951, 3182.699951, 3537700),
                                },
                _ => null,
            };

            return Task.FromResult(prices == null
                                    ? new PriceListResult(null, "Not found")
                                    : new PriceListResult(prices, null));
        }

        /// <inheritdoc/>
        public Task<SymbolListResult> GetAllSymbolsAsync(CancellationToken cancellationToken)
        {
            var symbols = new SymbolListResult(
                                new List<SymbolListItem>()
                                {
                                    new("AAPL", "Apple Inc", "NASDAQ"),
                                    new("MSFT", "Microsoft Corp", "NASDAQ"),
                                    new("AMZN", "Amazon.com Inc", "NASDAQ"),
                                    ////new("GOOG", "Alphabet Cl C", "NASDAQ"),
                                    ////new("FB", "Facebook Inc", "NASDAQ"),
                                    ////new("TSLA", "Tesla Inc", "NASDAQ"),
                                    ////new("NVDA", "Nvidia Corp", "NASDAQ"),
                                },
                                DateTime.Now);

            return Task.FromResult(symbols);
        }
    }
}