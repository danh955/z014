// <copyright file="DownloadYahooFinanceService.StockPrices.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Download.YahooFinance
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using Hilres.Stock.Download.Abstraction;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Download Yahoo Finance service class for stock prices.
    /// </summary>
    public partial class DownloadYahooFinanceService
    {
        private readonly DateTime epoch = new(1970, 1, 1, 0, 0, 0);
        private int count = 0;

        /// <inheritdoc/>
        public async Task<PriceListResult> GetStockPricesAsync(string symbol, DateTime firstDate, DateTime lastDate, StockInterval interval, CancellationToken cancellationToken)
        {
            await this.token.RefreshAsync(symbol, this.httpClient, cancellationToken);

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

            string uri = $"https://query1.finance.yahoo.com/v7/finance/download/{symbol}?period1={epochFirstDate}&period2={epochLastDate}&interval={intervalString}&events=history&includeAdjustedClose=true&crumb={this.token.Crumb}";

            List<PriceListItem> items = new();

            try
            {
                this.count++;
                this.logger.LogDebug("RequestCount = {0}, Symbol = {1}, ", this.count, symbol);

                bool successful = false;
                while (!successful && !cancellationToken.IsCancellationRequested)
                {
                    var response = await this.httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                        using var streamReader = new StreamReader(responseStream);
                        using var csv = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture));

                        if (!cancellationToken.IsCancellationRequested && await csv.ReadAsync().ConfigureAwait(false))
                        {
                            csv.ReadHeader();

                            while (!cancellationToken.IsCancellationRequested && await csv.ReadAsync().ConfigureAwait(false))
                            {
                                var a = csv[0];
                                items.Add(new PriceListItem(
                                    Date: csv.GetField<DateTime>(0),
                                    Open: csv.GetDouble(1),
                                    High: csv.GetDouble(2),
                                    Low: csv.GetDouble(3),
                                    Close: csv.GetDouble(4),
                                    AdjClose: csv.GetDouble(5),
                                    Volume: csv.GetLong(6)));
                            }
                        }

                        successful = true;
                    }
                    else
                    {
                        string errorMessage = $"StatusCode={response.StatusCode}, ReasonPhrase={response.ReasonPhrase}";
                        this.logger.LogWarning(errorMessage);

                        if (response.StatusCode != HttpStatusCode.Unauthorized)
                        {
                            return new PriceListResult(items, errorMessage);
                        }

                        await this.token.ClearAndRefreshAsync(symbol, this.httpClient, cancellationToken);
                        successful = false;
                    }
                }
            }
            catch (HttpRequestException e)
            {
                this.logger.LogWarning(e, "HttpRequestException");
                return new PriceListResult(items, e.Message);
            }
            catch (TypeConverterException e)
            {
                this.logger.LogWarning(e, "TypeConverterException");
                return new PriceListResult(items, e.Message);
            }

            await Task.Delay(10, cancellationToken);
            return new PriceListResult(items);
        }
    }
}