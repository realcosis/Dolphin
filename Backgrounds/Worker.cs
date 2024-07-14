using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Dolphin.Backgrounds
{
    public class Worker(ILogger<Worker> logger, IBackgroundManager backgroundManager) : BackgroundService
    {
        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = backgroundManager.Dequeue();

                try
                {
                    if (workItem != default)
                        await workItem.Execute();
                }
                catch (Exception ex)
                {
                    logger.LogError("Error occurred executing {workItem}: {ex}", nameof(workItem), ex);
                }

                await Task.Yield();
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
            => await BackgroundProcessing(cancellationToken);

        public override async Task StopAsync(CancellationToken cancellationToken)
            => await base.StopAsync(cancellationToken);
    }
}