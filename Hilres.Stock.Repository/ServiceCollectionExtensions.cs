// <copyright file="ServiceCollectionExtensions.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Repository
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service collection extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Hilres stock repository service to the service collection.
        /// </summary>
        /// <param name="service">IServiceCollection.</param>
        /// <param name="connectionString">LiteDB connection string.</param>
        /// <returns>Updated IServiceCollection.</returns>
        public static IServiceCollection AddHilresStockRepositoryService(this IServiceCollection service, string connectionString)
        {
            service.AddSingleton<StockRepositoryService>(new StockRepositoryService(connectionString));
            return service;
        }
    }
}