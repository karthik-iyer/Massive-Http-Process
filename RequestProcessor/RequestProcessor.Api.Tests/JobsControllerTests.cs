using Microsoft.AspNetCore.Mvc;
using Moq;
using RequestProcessor.Api.Controllers;
using RequestProcessor.Core.Models;
using RequestProcessor.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RequestProcessor.Api.Tests
{
    public class JobsControllerTests
    {
        private readonly Mock<IRequestProcessService> _requestProcessServiceMock = new Mock<IRequestProcessService>();
        private readonly JobsController _jobsController;
        public JobsControllerTests()
        {
            _jobsController = new JobsController(_requestProcessServiceMock.Object);
        }

        [Fact]
        public async Task CreateJob_Fails_ThrowsBadRequest()
        {
            var httpRequestModel = new HttpRequestModel();
            //Arrange
            _requestProcessServiceMock.Setup(x => x.ProcessBackgroundJob(It.IsAny<HttpRequestModel>())).ThrowsAsync(new Exception());

            //Act
            var result = await _jobsController.CreateJob(httpRequestModel);


            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateJob_Succeeds_ReturnsId()
        {
            var httpRequestModel = new HttpRequestModel();
            //Arrange
            _requestProcessServiceMock.Setup(x => x.ProcessBackgroundJob(It.IsAny<HttpRequestModel>())).ReturnsAsync(Guid.NewGuid().ToString());

            //Act
            var result = await _jobsController.CreateJob(httpRequestModel);


            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetJobStatus_Fails_ThrowsBadRequest()
        {
            var httpRequestModel = new HttpRequestModel();
            //Arrange
            _requestProcessServiceMock.Setup(x => x.GetJobStatus(It.IsAny<string>())).ThrowsAsync(new Exception());

            //Act
            var result = await _jobsController.GetJobStatus(Guid.NewGuid().ToString());


            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetJobStatus_Succeeds_ReturnsId()
        {
            var httpRequestModel = new HttpRequestModel();
            //Arrange
            _requestProcessServiceMock.Setup(x => x.GetJobStatus(It.IsAny<string>())).ReturnsAsync(new HttpResponseModel());

            //Act
            var result = await _jobsController.GetJobStatus(Guid.NewGuid().ToString());


            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
