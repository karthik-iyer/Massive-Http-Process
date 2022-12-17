using Cloudmersive.APIClient.NET.Validate.Api;
using Moq;
using RequestProcessor.Core.Models;
using RequestProcessor.Data.Interfaces;
using RequestProcessor.Services.BackgroundServices;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RequestProcessor.Services.Test
{
    public class RequestProcessServiceTests
    {
        private readonly Mock<IJobRepository> _jobRepositoryMock = new Mock<IJobRepository>();
        private readonly Mock<IDomainApi> _domainApiMock = new Mock<IDomainApi>();

        private readonly RequestProcessService _requestProcessService;

        public RequestProcessServiceTests()
        {
            _requestProcessService = new RequestProcessService(_jobRepositoryMock.Object,_domainApiMock.Object);
            
        }

        [Fact]
        public async Task GetJobStatus_Returns_JobStatus_GivenJobId()
        {
            //Arrange
            _jobRepositoryMock.Setup(x => x.GetJob(It.IsAny<string>())).ReturnsAsync(new JobModel() { CurrentJobStatus = JobStatus.FINISHED_SUCCESS });

            //Act
            var httpResponseModel  = await _requestProcessService.GetJobStatus(Guid.NewGuid().ToString());


            //Assert
            Assert.Equal("FINISHED_SUCCESS", httpResponseModel.JobStatus);
        }


        [Fact]
        public async Task ProcessBackgroundJob_Returns_JobId()
        {
            //Arrange
            _jobRepositoryMock.Setup(x => x.IsClientIdRunningJobsMoreThanTwo(It.IsAny<string>())).ReturnsAsync(true);
            _jobRepositoryMock.Setup(x => x.SaveJob(It.IsAny<JobModel>())).ReturnsAsync(Guid.NewGuid().ToString());

            //Act
            var result = await _requestProcessService.ProcessBackgroundJob(new HttpRequestModel{ ClientId = Guid.NewGuid()},It.IsAny<IBackgroundTaskQueue>());


            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task ProcessBackgroundJob_LessThan2ClientswithSameIdRunning_ReturnsJobId()
        {
            //Arrange
            _jobRepositoryMock.Setup(x => x.IsClientIdRunningJobsMoreThanTwo(It.IsAny<string>())).ReturnsAsync(false);
            _jobRepositoryMock.Setup(x => x.SaveJob(It.IsAny<JobModel>())).ReturnsAsync(Guid.NewGuid().ToString());
            var mockBackgroundTaskQueue = new Mock<IBackgroundTaskQueue>();
            //Act
            var result = await _requestProcessService.ProcessBackgroundJob(new HttpRequestModel() { ClientId = Guid.NewGuid()},mockBackgroundTaskQueue.Object);


            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task ProcessBackgroundJob_IsMaxNumberOfJobsInRunningState_False_ReturnsJobId()
        {
            //Arrange
            _jobRepositoryMock.Setup(x => x.IsClientIdRunningJobsMoreThanTwo(It.IsAny<string>())).ReturnsAsync(false);
            _jobRepositoryMock.Setup(x => x.IsMaxNumberOfJobsInRunningState()).ReturnsAsync(false);
            _jobRepositoryMock.Setup(x => x.SaveJob(It.IsAny<JobModel>())).ReturnsAsync(Guid.NewGuid().ToString());

            var mockBackgroundTaskQueue = new Mock<IBackgroundTaskQueue>();
            //Act
            var result = await _requestProcessService.ProcessBackgroundJob(new HttpRequestModel() { ClientId = Guid.NewGuid() }, mockBackgroundTaskQueue.Object);


            //Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task ProcessBackgroundJob_IsMaxNumberOfJobsInRunningState_True_ReturnsJobId()
        {
            //Arrange
            _jobRepositoryMock.Setup(x => x.IsClientIdRunningJobsMoreThanTwo(It.IsAny<string>())).ReturnsAsync(true);
            _jobRepositoryMock.Setup(x => x.IsMaxNumberOfJobsInRunningState()).ReturnsAsync(false);
            _jobRepositoryMock.Setup(x => x.SaveJob(It.IsAny<JobModel>())).ReturnsAsync(Guid.NewGuid().ToString());

            //Act
            var result = await _requestProcessService.ProcessBackgroundJob(new HttpRequestModel() { ClientId = Guid.NewGuid() }, It.IsAny<IBackgroundTaskQueue>());


            //Assert
            Assert.IsType<string>(result);
        }
    }
}
