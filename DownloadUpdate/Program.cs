// <copyright file="Program.cs" company="None">
// Free and open source code.
// </copyright>

namespace DownloadUpdate
{
    using System.Threading.Tasks;
    using Hilres.Stock.Download.YahooFinance;
    using Hilres.Stock.DownloadUpdateDatabase;
    using Hilres.Stock.Repository;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Start of main program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Task.</returns>
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseConsoleLifetime()
                .ConfigureServices((builder, services) =>
                    {
                        services.AddHilresStockDownloadYahooFinanceService();
                        services.AddHilresStockRepositoryService(builder.Configuration["LiteDB:ConnectionString"]);
                        services.AddHilresStockUpdater();
                        services.AddHostedService<MainAppService>();
                    })
                .RunConsoleAsync();
        }
    }
}