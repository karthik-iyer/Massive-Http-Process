using RequestProcessor.Core.Models;
using System;
using System.Threading.Tasks;

namespace RequestProcessor.Data.Interfaces
{
    public interface IJobRepository
    {
        Task<string> SaveJob(JobModel jobModel);

        Task<JobModel> UpdateJob(JobModel jobModel);

        Task<JobModel> GetJob(string JobId);

        Task<bool> IsClientIdRunningJobsMoreThanTwo(string ClientId);


        Task<bool> IsMaxNumberOfJobsInRunningState();


    }
}
