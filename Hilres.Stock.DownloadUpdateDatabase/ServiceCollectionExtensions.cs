// <copyright file="ServiceCollectionExtensions.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.DownloadUpdateDatabase
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service collection extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the download stock data and update database service to the service collection.
        /// </summary>
        /// <param name="service">IServiceCollection.</param>
        /// <returns>Updated IServiceCollection.</returns>
        public static IServiceCollection AddHilresStockUpdater(this IServiceCollection service)
        {
            service.AddSingleton<DownloadUpdateDatabaseService>();
            return service;
        }
    }
}