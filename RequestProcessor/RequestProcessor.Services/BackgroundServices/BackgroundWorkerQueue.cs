using RequestProcessor.Core.Models;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace RequestProcessor.Services.BackgroundServices
{
    public class BackgroundWorkerQueue : IBackgroundTaskQueue
    {
        private ConcurrentQueue<JobModel> _workItems = new ConcurrentQueue<JobModel>();
        private SemaphoreSlim _signal = new SemaphoreSlim(0);

        public async Task<JobModel> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }

        public void QueueBackgroundWorkItem(JobModel workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }
    }
}
