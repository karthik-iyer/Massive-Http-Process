using Microsoft.AspNetCore.Mvc;
using RequestProcessor.Core.Models;
using RequestProcessor.Services.BackgroundServices;
using RequestProcessor.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace RequestProcessor.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [ApiVersion("1.0")]
    public class JobsController : ControllerBase
    {
        private readonly IRequestProcessService _requestProcessService;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;

        public JobsController(IRequestProcessService requestProcessService, IBackgroundTaskQueue backgroundTaskQueue)
        {
            _requestProcessService = requestProcessService;
            _backgroundTaskQueue = backgroundTaskQueue;
        }


        [HttpPost]
        [Route("create-job")]
        public async Task<ActionResult<string>> CreateJob(HttpRequestModel httpRequestModel)
        {           
            
            try
            {
                var jobId = await _requestProcessService.ProcessBackgroundJob(httpRequestModel, _backgroundTaskQueue);
                return Ok(jobId);

            }
            catch(Exception ex)
            {

                return BadRequest($"Error Occured in Create Job. {ex}");
            }

        }


        [HttpGet]
        [Route("check-job-status/{jobId}")]
        public async Task<ActionResult<HttpResponseModel>> GetJobStatus(string jobId)
        {
           
            try
            {
                var httpResponseModel = await _requestProcessService.GetJobStatus(jobId);
                return Ok(httpResponseModel);

            }
            catch (Exception ex)
            {

                return BadRequest($"Error Occured in Check Job Status. {ex}");
            }

        }

    }
}
