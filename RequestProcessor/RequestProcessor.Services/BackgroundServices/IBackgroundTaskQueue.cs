using RequestProcessor.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RequestProcessor.Services.BackgroundServices
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(JobModel workItem);

        Task<JobModel> DequeueAsync(
            CancellationToken cancellationToken);
    }
    
}
