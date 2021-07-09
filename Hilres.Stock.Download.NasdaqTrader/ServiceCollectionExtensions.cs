﻿// <copyright file="ServiceCollectionExtensions.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Download.NasdaqTrader
{
    using Hilres.Stock.Download.Abstraction;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service collection extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Stock data source from Yahoo financial to the service collection.
        /// </summary>
        /// <param name="service">IServiceCollection.</param>
        /// <returns>Updated IServiceCollection.</returns>
        public static IServiceCollection AddHilresStockDataSourceYahoo(this IServiceCollection service)
        {
            service.AddHttpClient();
            service.AddTransient<IStockDownloadService, DownloadNasdaqTraderService>();
            return service;
        }
    }
}