using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RequestProcessor.Services.BackgroundServices
{
    public class BackgroundProcessingService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _queue;

        public BackgroundProcessingService(IBackgroundTaskQueue queue)
        {
            _queue = queue;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _queue.DequeueAsync(stoppingToken);

                try
                {
                    //Process LongRunning Task with JobModel

                   

                }
                catch (Exception ex)
                {
                    //Logging any exception
                    throw;
                   
                }
            }
        }
    }
}
