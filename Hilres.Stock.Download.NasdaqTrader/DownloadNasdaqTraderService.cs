// <copyright file="DownloadNasdaqTraderService.cs" company="None">
// Free and open source code.
// </copyright>
#pragma warning disable SA1118 // Parameter should not span multiple lines

namespace Hilres.Stock.Download.NasdaqTrader
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Hilres.Stock.Download.Abstraction;

    /// <summary>
    /// Download NASDAQ trader service class.
    /// </summary>
    public class DownloadNasdaqTraderService : IStockDownloadService
    {
        private const string FileCreationTimeText = @"File Creation Time:";
        private const string NasdaqListedUri = @"http://www.nasdaqtrader.com/dynamic/SymDir/nasdaqlisted.txt";
        private const string OtherListedUri = @"http://www.nasdaqtrader.com/dynamic/SymDir/otherlisted.txt";
        private readonly IHttpClientFactory clientFactory;

        private readonly IEnumerable<string> includeExchangeCodes = new List<string> { "N" };

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadNasdaqTraderService"/> class.
        /// </summary>
        /// <param name="clientFactory">IHttpClientFactory.</param>
        public DownloadNasdaqTraderService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        /// <summary>
        /// Get all the symbols.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>NasdaqSymbolsResult.</returns>
        public async Task<ISymbolListResult> GetSymbolListAsync(CancellationToken cancellationToken)
        {
            var csvConfigurationPipe = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "|" };

            var nasdaqSymbolTask = this.GetItemsAsync(
                   uri: NasdaqListedUri,
                   csvConfiguration: csvConfigurationPipe,
                   cancellationToken: cancellationToken,
                   createItem: (csv) =>
                   {
                       // Is this a test symbol?
                       if (Parse.IsTrue(csv.GetField(3)))
                       {
                           return null;
                       }

                       return new(
                           symbol: csv[0].Trim(),
                           securityName: csv[1].Trim(),
                           exchange: "NASDAQ");
                   });

            var otherSymbolTask = this.GetItemsAsync(
               uri: OtherListedUri,
               csvConfiguration: csvConfigurationPipe,
               cancellationToken: cancellationToken,
               createItem: (csv) =>
               {
                   // Is this a test symbol?
                   if (Parse.IsTrue(csv.GetField(6)))
                   {
                       return null;
                   }

                   string exchange = csv[2].ToUpper();
                   if (!this.includeExchangeCodes.Contains(exchange))
                   {
                       return null;
                   }

                   return new(
                       symbol: csv[7].Trim(),
                       securityName: csv[1].Trim(),
                       exchange: exchange switch
                       {
                           "A" => "NYSE MKT",
                           "N" => "NYSE",  // New York Stock Exchange
                           "P" => "NYSE ARCA",
                           "Z" => "BATS",  // BATS Global Markets
                           "V" => "IEXG",  // Investors Exchange, LLC
                           _ => csv[2],
                       });
               });

            await Task.WhenAll(nasdaqSymbolTask, otherSymbolTask);

            return new SymbolListResult(
                symbols: nasdaqSymbolTask.Result.Item1.Union(otherSymbolTask.Result.Item1),
                fileCreationTime: Max(nasdaqSymbolTask.Result.Item2, otherSymbolTask.Result.Item2));
        }

        private static DateTime Max(DateTime value1, DateTime value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Get a list of items.
        /// </summary>
        /// <param name="uri">URL to get the data.</param>
        /// <param name="csvConfiguration">CSV configuration.</param>
        /// <param name="createItem">Function to create item.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>List of items.</returns>
        private async Task<Tuple<IEnumerable<SymbolListItem>, DateTime>> GetItemsAsync(
            string uri,
            CsvConfiguration csvConfiguration,
            Func<CsvReader, SymbolListItem> createItem,
            CancellationToken cancellationToken)
        {
            List<SymbolListItem> items = new();
            DateTime fileCreationTime = default;

            HttpClient httpClient = this.clientFactory.CreateClient();
            using var responseStream = await httpClient.GetStreamAsync(uri, cancellationToken);
            using var streamReader = new StreamReader(responseStream);

            using var csv = new CsvReader(streamReader, csvConfiguration);

            if (!cancellationToken.IsCancellationRequested && await csv.ReadAsync())
            {
                csv.ReadHeader();

                while (!cancellationToken.IsCancellationRequested && await csv.ReadAsync())
                {
                    if (csv[0].StartsWith(FileCreationTimeText))
                    {
                        fileCreationTime = Parse.FileCreationTime(csv[0][FileCreationTimeText.Length..]);
                    }
                    else
                    {
                        var newItem = createItem(csv);
                        if (newItem != null)
                        {
                            items.Add(newItem);
                        }
                    }
                }
            }

            return new(items, fileCreationTime);
        }
    }
}