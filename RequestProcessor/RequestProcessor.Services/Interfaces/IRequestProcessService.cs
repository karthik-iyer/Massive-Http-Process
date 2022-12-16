using RequestProcessor.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RequestProcessor.Services.Interfaces
{
    public interface IRequestProcessService
    {
        Task<string> ProcessBackgroundJob(HttpRequestModel httpRequestModel);

        Task<HttpResponseModel> GetJobStatus(string jobId);

    }
}
