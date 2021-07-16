// <copyright file="MainAppService.cs" company="None">
// Free and open source code.
// </copyright>

namespace DownloadUpdate
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.DownloadUpdateDatabase;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Main application service class.
    /// </summary>
    public class MainAppService : BackgroundService
    {
        private readonly IHostApplicationLifetime appLifetime;
        private readonly ILogger logger;
        private readonly DownloadUpdateDatabaseService stockUpdater;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainAppService"/> class.
        /// </summary>
        /// <param name="appLifetime">IHostApplicationLifetime.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="stockUpdater">DownloadUpdateDatabaseService.</param>
        public MainAppService(IHostApplicationLifetime appLifetime, DownloadUpdateDatabaseService stockUpdater, ILogger<MainAppService> logger)
        {
            this.appLifetime = appLifetime;
            this.logger = logger;
            this.stockUpdater = stockUpdater;
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Begin");

            await this.stockUpdater.UpdataDb(cancellationToken);

            this.appLifetime.StopApplication();
            this.logger.LogInformation("End");
        }
    }
}