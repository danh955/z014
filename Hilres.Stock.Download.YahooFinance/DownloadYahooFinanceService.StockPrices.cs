// <copyright file="DownloadYahooFinanceService.StockPrices.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Hilres.Stock.Download.Abstraction;

    /// <summary>
    /// Download Yahoo Finance service class for stock prices.
    /// </summary>
    public partial class DownloadYahooFinanceService
    {
        private readonly DateTime epoch = new(1970, 1, 1, 0, 0, 0);

        /// <inheritdoc/>
        public async Task<PriceListResult> GetStockPricesAsync(string symbol, DateTime firstDate, DateTime lastDate, StockInterval interval, CancellationToken cancellationToken)
        {
            long epochFirstDate = (long)(firstDate.Date - this.epoch).TotalSeconds;
            long epochLastDate = (long)(lastDate.Date - this.epoch).TotalSeconds;
            string intervalString = interval switch
            {
                StockInterval.Daily => "1d",
                StockInterval.Weekly => "1wk",
                StockInterval.Monthly => "1mo",
                StockInterval.Quorterly => "3mo",
                _ => throw new NotImplementedException(interval.ToString()),
            };

            string uri = $"https://query1.finance.yahoo.com/v7/finance/download/{symbol}?period1={epochFirstDate}&period2={epochLastDate}&interval={intervalString}&events=history&includeAdjustedClose=true";

            List<PriceListItem> items = new();

            try
            {
                HttpClient httpClient = this.httpClientFactory.CreateClient();
                using var responseStream = await httpClient.GetStreamAsync(uri, cancellationToken);
                using var streamReader = new StreamReader(responseStream);
                using var csv = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture));

                if (!cancellationToken.IsCancellationRequested && await csv.ReadAsync())
                {
                    csv.ReadHeader();

                    while (!cancellationToken.IsCancellationRequested && await csv.ReadAsync())
                    {
                        items.Add(new PriceListItem(
                            Date: csv.GetField<DateTime>(0),
                            Open: csv.GetField<double>(1),
                            High: csv.GetField<double>(2),
                            Low: csv.GetField<double>(3),
                            Close: csv.GetField<double>(4),
                            AdjClose: csv.GetField<double>(5),
                            Volume: csv.GetField<long>(6)));
                    }
                }
            }
            catch (HttpRequestException e)
            {
                return new PriceListResult(null, e.Message);
            }

            await Task.Delay(100);
            return new PriceListResult(items);
        }
    }
}