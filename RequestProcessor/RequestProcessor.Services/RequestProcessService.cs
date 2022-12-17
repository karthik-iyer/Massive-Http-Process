using Cloudmersive.APIClient.NET.Validate.Api;
using RequestProcessor.Core.Models;
using RequestProcessor.Data.Interfaces;
using RequestProcessor.Services.BackgroundServices;
using RequestProcessor.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace RequestProcessor.Services
{
    public class RequestProcessService : IRequestProcessService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IDomainApi _domainApi;
        private ConcurrentQueue<JobModel> _queuedItems = new ConcurrentQueue<JobModel>();

        public RequestProcessService(IJobRepository jobRepository,IDomainApi domainApi)
        {
            _jobRepository = jobRepository;
            _domainApi = domainApi;

        }

        /// <summary>
        /// Getting the JobStatus based on JobId
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task<HttpResponseModel> GetJobStatus(string jobId)
        {
            var httpResponseModel = new HttpResponseModel();

            var jobModel = await _jobRepository.GetJob(jobId);

            httpResponseModel.JobStatus = jobModel.CurrentJobStatus.ToString();

            return httpResponseModel;
        }

        /// <summary>
        /// Processing Background Job for a given Http Request
        /// </summary>
        /// <param name="httpRequestModel"></param>
        /// <returns></returns>
        public async Task<string> ProcessBackgroundJob(HttpRequestModel httpRequestModel, IBackgroundTaskQueue backgroundTaskQueue)
        {
            var jobId = Guid.NewGuid().ToString();

            var jobModel = new JobModel
            {
                JobId = jobId
            };

            if (!await ValidateRequest(httpRequestModel))
            {
                jobModel.CurrentJobStatus = JobStatus.FINISHED_FAILED;
                jobId = await _jobRepository.SaveJob(jobModel);
            }
            else
            {

                if (await ValidateExistingClientIdInProcess(httpRequestModel))
                {
                    jobModel.CurrentJobStatus = JobStatus.NOT_STARTED;
                    jobId = await _jobRepository.SaveJob(jobModel);
                    //queue the job
                    _queuedItems.Enqueue(jobModel);

                }
                else if (await _jobRepository.IsMaxNumberOfJobsInRunningState())
                {
                    jobModel.CurrentJobStatus = JobStatus.NOT_STARTED;
                    jobId = await _jobRepository.SaveJob(jobModel);
                    //Queue the job
                    _queuedItems.Enqueue(jobModel);
                }
                else
                {
                    
                    jobModel.CurrentJobStatus = JobStatus.STARTED;
                    jobId = await _jobRepository.SaveJob(jobModel);
                    //Run the job in the background
                    backgroundTaskQueue.QueueBackgroundWorkItem(jobModel);

                    //check if the number of current running items in background is <8 then dequeue from above queue and enqueue for background processing
                    _queuedItems.TryDequeue(out JobModel waitingJobModel);

                    backgroundTaskQueue.QueueBackgroundWorkItem(waitingJobModel);
                }
            }

            //Return JobId Immediately after calling background process or queueing the job
            return jobId;
            
        }

        /// <summary>
        /// Validating if 2 instances of Existing ClientId is in running then queue that job
        /// </summary>
        /// <param name="httpRequestModel"></param>
        /// <returns></returns>
        private async Task<bool> ValidateExistingClientIdInProcess(HttpRequestModel httpRequestModel)
        {
            var IsClientIdActivelyRunning = await _jobRepository.IsClientIdRunningJobsMoreThanTwo(httpRequestModel.ClientId.ToString());
            return IsClientIdActivelyRunning;
        }

        /// <summary>
        /// Validating Request for security and vulnerability issues using Cloudmersive SDK
        /// </summary>
        /// <param name="httpRequestModel"></param>
        /// <returns></returns>
        private async Task<bool> ValidateRequest(HttpRequestModel httpRequestModel)
        {
            //Call Cloudmersive SDK Here for Validating Security Issues like SSRF. Call the below api . This is commented due
            //await _domainApi.DomainSsrfCheckAsync(new Cloudmersive.APIClient.NET.Validate.Model.UrlSsrfRequestFull() { URL = "https://test.com", BlockedDomains = new System.Collections.Generic.List<string>() {""}});

            //Validate Input parameters 
            //Return true if validation passed else false

            return await Task.FromResult(true);
        }


    }
}
