// <copyright file="YahooPriceHistoryHelper.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Download.YahooFinance.Tests.Helper
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Moq;

    /// <summary>
    /// Yahoo price history helper class.
    /// </summary>
    public static class YahooPriceHistoryHelper
    {
        private static readonly Dictionary<string, HttpResponseMessage> Messages = new()
        {
            {
                //// Mar 09, 2021 - Mar 14, 2021, Historical Prices, Daily
                "https://query1.finance.yahoo.com/v7/finance/download/MSFT?period1=1615248000&period2=1615680000&interval=1d&events=history&includeAdjustedClose=true",
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"Date,Open,High,Low,Close,Adj Close,Volume
2021-03-09,232.880005,235.380005,231.669998,233.779999,233.779999,33034000
2021-03-10,237.000000,237.000000,232.039993,232.419998,232.419998,29733000
2021-03-11,234.960007,239.169998,234.309998,237.130005,237.130005,29896000
2021-03-12,234.009995,235.820007,233.229996,235.750000,235.750000,22647900
"),
                }
            },
            {
                //// Dec 14, 2020 - Mar 14, 2021, Historical Prices, Weekly
                "https://query1.finance.yahoo.com/v7/finance/download/MSFT?period1=1607904000&period2=1615680000&interval=1wk&events=history&includeAdjustedClose=true",
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"Date,Open,High,Low,Close,Adj Close,Volume
2020-12-14,213.100006,220.889999,212.240005,218.589996,218.087708,186710500
2020-12-21,217.550003,225.630005,217.279999,222.750000,222.238144,89044300
2020-12-28,224.449997,227.179993,219.679993,222.419998,221.908905,76551100
2021-01-04,222.529999,223.000000,211.940002,219.619995,219.115341,147534500
2021-01-11,218.470001,218.910004,212.029999,212.649994,212.161346,127610700
2021-01-18,213.750000,230.070007,212.630005,225.949997,225.430786,129180500
2021-01-25,229.119995,242.639999,224.220001,231.960007,231.426987,243772400
2021-02-01,235.059998,245.089996,232.429993,242.199997,241.643448,129728600
2021-02-08,243.149994,245.919998,240.809998,244.990005,244.427048,100257800
2021-02-15,245.029999,246.130005,240.179993,240.970001,240.416275,90570200
2021-02-22,237.419998,237.929993,227.880005,232.380005,232.380005,170368700
2021-03-01,235.899994,237.470001,224.259995,231.600006,231.600006,168486200
2021-03-08,231.369995,239.169998,227.130005,235.750000,235.750000,150556800
2021-03-12,234.009995,235.820007,233.229996,235.750000,235.750000,22653662
"),
                }
            },
            {
                //// Oct 1, 2020 - Mar 14, 2021, Historical Prices, Monthly
                "https://query1.finance.yahoo.com/v7/finance/download/MSFT?period1=1633046400&period2=1615680000&interval=1mo&events=history&includeAdjustedClose=true",
                new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"Date,Open,High,Low,Close,Adj Close,Volume
2020-10-01,213.490005,225.210007,199.619995,202.470001,201.477280,631648000
2020-11-01,204.289993,228.119995,200.119995,214.070007,213.020401,573443000
2020-12-01,214.509995,227.179993,209.110001,222.419998,221.908905,594806000
2021-01-01,222.529999,242.639999,211.940002,231.960007,231.426987,648098100
2021-02-01,235.059998,246.130005,227.880005,232.380005,231.846024,490925300
2021-03-01,235.899994,239.169998,224.259995,235.750000,235.750000,319043000
2021-03-12,234.009995,235.820007,233.229996,235.750000,235.750000,22653662
"),
                }
            },
        };

        /// <summary>
        /// Mock IHttpClientFactory for YahooPriceHistoryTest with data.
        /// </summary>
        /// <returns>IHttpClientFactory.</returns>
        public static IHttpClientFactory MockIHttpClientFactory()
        {
            HttpClient httpClient = new(new MockHttpResponseMessage(Messages));

            var mock = new Mock<IHttpClientFactory>();
            mock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient ?? new HttpClient());
            return mock.Object;
        }
    }
}