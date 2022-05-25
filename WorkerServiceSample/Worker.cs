using Microsoft.Extensions.Options;

namespace WorkerServiceSample
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerConfiguration _config;

        public Worker(ILogger<Worker> logger, IOptions<WorkerConfiguration> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ServiceWorker starting");
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ServiceWorker finished");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var secondsToWaitFirstTime = HelpersLibrary.CalculateFirstTimeStart(_config.WorkerStartTime);
            _logger.LogInformation("Next execution at " + secondsToWaitFirstTime + " seconds");
            // wait till midnight
            await Task.Delay(TimeSpan.FromSeconds(secondsToWaitFirstTime), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                // do stuff
                _logger.LogInformation($"Starting process at: " + DateTime.Now.ToString());

                // wait 24 hours
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}