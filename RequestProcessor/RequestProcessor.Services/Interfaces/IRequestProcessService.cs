using RequestProcessor.Core.Models;
using RequestProcessor.Services.BackgroundServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RequestProcessor.Services.Interfaces
{
    public interface IRequestProcessService
    {
        Task<string> ProcessBackgroundJob(HttpRequestModel httpRequestModel, IBackgroundTaskQueue _backgroundTaskQueue);

        Task<HttpResponseModel> GetJobStatus(string jobId);

    }
}
