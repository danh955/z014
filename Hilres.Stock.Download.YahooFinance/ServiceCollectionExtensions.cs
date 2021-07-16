// <copyright file="ServiceCollectionExtensions.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Download.YahooFinance
{
    using Hilres.Stock.Download.Abstraction;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service collection extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Hilres stock download Yahoo Finance service to the service collection.
        /// </summary>
        /// <param name="service">IServiceCollection.</param>
        /// <returns>Updated IServiceCollection.</returns>
        public static IServiceCollection AddHilresStockDownloadYahooFinanceService(this IServiceCollection service)
        {
            service.AddHttpClient();
            service.AddTransient<IStockDownloadService, DownloadYahooFinanceService>();
            return service;
        }
    }
}