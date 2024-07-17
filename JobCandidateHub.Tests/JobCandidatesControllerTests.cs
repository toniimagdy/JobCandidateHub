using JobCandidateHub.DataAccess.Models;
using JobCandidateHub.Models;
using JobCandidateHub.Services.IServices;
using JobCandidateHub.Services.Services;
using JobCandidateHub.WebAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace JobCandidateHub.Tests
{
    public class JobCandidatesControllerTests
    {
        private readonly Mock<IJobCandidateService> _mockService;
        private readonly Mock<ILogger<JobCandidatesController>> _mockLogger;
        private readonly JobCandidatesController _controller;

        public JobCandidatesControllerTests()
        {
            _mockService = new Mock<IJobCandidateService>();
            _mockLogger = new Mock<ILogger<JobCandidatesController>>();
            _controller = new JobCandidatesController(_mockService.Object, _mockLogger.Object);
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };
        }


        private void AddModelError(string key, string errorMessage)
        {
            _controller.ModelState.AddModelError(key, errorMessage);
        }


        [Fact]
        public async Task Upsert_ValidCandidate_ReturnsOk()
        {
            // Arrange
            var candidate = new JobCandidateModel
            {
                FirstName = "Test",
                LastName = "Candidate",
                PhoneNumber = "123-456-7890",
                Email = "test.candidate@example.com",
                PreferredCallTimeFrom = "16:00:00",
                PreferredCallTimeTo = "18:00:00",
                LinkedInProfileUrl = "https://www.linkedin.com/in/testcandidate",
                GitHubProfileUrl = "https://github.com/testcandidate",
                Comment = "Experienced software developer."
            };
            _mockService.Setup(s => s.UpsertJobCandidateAsync(candidate)).ReturnsAsync(candidate);

            // Act
            var result = await _controller.Upsert(candidate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<JobCandidateModel>(okResult.Value);
            Assert.Equal(candidate.FirstName, returnValue.FirstName);
        }

        [Fact]
        public async Task Upsert_InvalidCandidate_ReturnsBadRequest()
        {
            // Arrange

            var invalidCandidate = new JobCandidateModel
            {
                FirstName = "",
                LastName = "",
                Email = "john.doe@example.com",
                PreferredCallTimeFrom = "09:00:00",
                PreferredCallTimeTo = "17:00:00"
            };

            AddModelError("FirstName", "First Name is required.");
            AddModelError("LastName", "Last Name is required.");
            AddModelError("Comment", "Comment is required.");

            // Act
            var result = await _controller.Upsert(invalidCandidate);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.True(returnValue.ContainsKey("FirstName"));
            Assert.True(returnValue.ContainsKey("LastName"));
            Assert.True(returnValue.ContainsKey("Comment"));
        }
    }
}
