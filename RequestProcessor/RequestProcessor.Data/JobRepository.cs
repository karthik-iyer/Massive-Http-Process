using RequestProcessor.Core.Models;
using RequestProcessor.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RequestProcessor.Data
{
    public class JobRepository : IJobRepository
    {
        //Ideally the JobRepository must inject the DB Context 
        //Using DBContext Make changes to the Database
        public JobRepository()
        {

        }

        /// <summary>
        /// For a given client Id verify if there are more than 2 simultaneously Running jobs 
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        public async Task<bool> IsClientIdRunningJobsMoreThanTwo(string ClientId)
        {
            //Read the number of jobs that are in Started status from DB 

            //return true if number of client Id running jobs are more than 2
            return await Task.FromResult(true);
        }

        /// <summary>
        /// For a given JobId get the Job from DB
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        public async Task<JobModel> GetJob(string JobId)
        {
            //Here there should be a read from the Database to get the job model corresponding to the JobId
            return await Task.FromResult( new JobModel() {  JobId = JobId, CurrentJobStatus = JobStatus.NOT_STARTED});
        }

        public async Task<string> SaveJob(JobModel jobModel)
        {
            //After the job is processed the Job needs to be Saved to the DB with latest status
            var jobId = Guid.NewGuid();
            return await Task.FromResult(jobId.ToString());
        }

        /// <summary>
        /// Update Job Status everytime the job is processed or queued or failed or timeout
        /// </summary>
        /// <param name="httpRequestModel"></param>
        /// <returns></returns>
        public async Task<JobModel> UpdateJob(JobModel jobModel)
        {
            //Call DB to update job and return jobModel with updated Status 
            return await Task.FromResult(jobModel);
           
        }

        /// <summary>
        /// Read the database to get the number of jobs with running state to Started
        /// </summary>
        /// <returns></returns>
        public Task<bool> IsMaxNumberOfJobsInRunningState()
        {
            throw new NotImplementedException();
        }
    }
}
