// <copyright file="MockData.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Download.NasdaqTrader.Tests.Helper
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using Moq;

    /// <summary>
    /// Mock data helper class.
    /// </summary>
    public static class MockData
    {
        /// <summary>
        /// Mock IHttpClientFactory for GetAllNasdaqAndOtherSymbols with data.
        /// </summary>
        /// <returns>IHttpClientFactory.</returns>
        public static IHttpClientFactory MockIHttpClientFactoryForNasdaqSymbols()
        {
            Dictionary<string, HttpResponseMessage> messages = new()
            {
                {
                    "http://www.nasdaqtrader.com/dynamic/SymDir/nasdaqlisted.txt",
                    new()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(@"Symbol|Security Name|Market Category|Test Issue|Financial Status|Round Lot Size|ETF|NextShares
AACG|ATA Creativity Global|G|N|N|100|N|N
CASY|Caseys General Stores, Inc.|Q|N|N|100|N|N
ESPO|VanEck Vectors Video Gaming and eSports ETF|G|N|N|100|Y|N
GDYN|Grid Dynamics Holdings, Inc.|S|N|N|100|N|N
JACK|Jack In The Box Inc. - Common Stock|Q|N|N|100|N|N
NFLX|Netflix, Inc. - Common Stock|Q|N|N|100|N|N
File Creation Time: 0212202121:32|||||||
"),
                    }
                },
                {
                    "http://www.nasdaqtrader.com/dynamic/SymDir/otherlisted.txt",
                    new()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(@"ACT Symbol|Security Name|Exchange|CQS Symbol|ETF|Round Lot Size|Test Issue|NASDAQ Symbol
A|Agilent Technologies|N|A|N|100|N|A
ECL|Ecolab Inc. Common Stock|N|ECL|N|100|N|ECL
HE|Hawaiian Electric Industries, Inc. Common Stock|N|HE|N|100|N|HE
LL|Lumber Liquidators Holdings, Inc Common Stock|N|LL|N|100|N|LL
PDT|John Hancock Premium Dividend Fund|N|PDT|N|100|N|PDT
TDOC|Teladoc Health, Inc. Common Stock|N|TDOC|N|100|N|TDOC
File Creation Time: 0212202121:32||||||
"),
                    }
                },
            };

            var httpClient = new HttpClient(new MockHttpResponseMessage(messages));

            var mock = new Mock<IHttpClientFactory>();
            mock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient ?? new HttpClient());
            return mock.Object;
        }
    }
}