using JobCandidateHub.Models;
using JobCandidateHub.DataAccess.Models;
using JobCandidateHub.DataAccess.Repositories;
using JobCandidateHub.DataAccess.UnitOfWork;
using JobCandidateHub.Services.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using JobCandidateHub.Services.IServices;

namespace JobCandidateHub.Tests
{
    public class JobCandidateServiceTests
    {
        private readonly Mock<IJobCandidateRepository> _mockRepo;
        private readonly Mock<IUnitOfWorkAsync> _mockUnitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IJobCandidateService _service;

        public JobCandidateServiceTests()
        {
            _mockRepo = new Mock<IJobCandidateRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWorkAsync>();
            _cache = new MemoryCache(new MemoryCacheOptions());
            _service = new JobCandidateService(_mockUnitOfWork.Object, _cache, _mockRepo.Object);
        }

        [Fact]
        public async Task UpsertJobCandidateAsync_CandidateExists_UpdatesCandidate()
        {
            // Arrange
            var candidateModel = new JobCandidateModel
            {
                Email = "existing@example.com",
                FirstName = "Existing",
                LastName = "User",
                PhoneNumber = "123-456-7890",
                PreferredCallTimeFrom = "09:00:00",
                PreferredCallTimeTo = "17:00:00",
                LinkedInProfileUrl = "https://www.linkedin.com/in/existinguser",
                GitHubProfileUrl = "https://github.com/existinguser",
                Comment = "Experienced software developer."
            };
            var candidateEntity = new JobCandidate
            {
                Email = "existing@example.com",
                FirstName = "Existing",
                LastName = "User",
                PhoneNumber = "098-765-4321",
                PreferredCallTimeFrom = TimeSpan.Parse("10:00:00"),
                PreferredCallTimeTo = TimeSpan.Parse("18:00:00"),
                LinkedInProfileUrl = "https://www.linkedin.com/in/existinguser",
                GitHubProfileUrl = "https://github.com/existinguser",
                FreeTextComment = "Experienced software developer."
            };

            _mockRepo.Setup(repo => repo.GetByEmailAsync(candidateModel.Email)).ReturnsAsync(candidateEntity);

            // Act
            var result = await _service.UpsertJobCandidateAsync(candidateModel);

            // Assert
            Assert.Equal(candidateModel.FirstName, result.FirstName);
            _mockRepo.Verify(repo => repo.UpdateAsync(It.Is<JobCandidate>(c =>
                c.FirstName == candidateModel.FirstName &&
                c.LastName == candidateModel.LastName &&
                c.Email == candidateModel.Email &&
                c.PhoneNumber == candidateModel.PhoneNumber &&
                c.PreferredCallTimeFrom == TimeSpan.Parse(candidateModel.PreferredCallTimeFrom) &&
                c.PreferredCallTimeTo == TimeSpan.Parse(candidateModel.PreferredCallTimeTo) &&
                c.LinkedInProfileUrl == candidateModel.LinkedInProfileUrl &&
                c.GitHubProfileUrl == candidateModel.GitHubProfileUrl &&
                c.FreeTextComment == candidateModel.Comment)), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpsertJobCandidateAsync_CandidateDoesNotExist_AddsCandidate()
        {
            // Arrange
            var candidateModel = new JobCandidateModel
            {
                Email = "new@example.com",
                FirstName = "New",
                LastName = "Candidate",
                PhoneNumber = "123-456-7890",
                PreferredCallTimeFrom = "09:00:00",
                PreferredCallTimeTo = "17:00:00",
                LinkedInProfileUrl = "https://www.linkedin.com/in/newcandidate",
                GitHubProfileUrl = "https://github.com/newcandidate",
                Comment = "Experienced software developer."
            };

            _mockRepo.Setup(repo => repo.GetByEmailAsync(candidateModel.Email)).ReturnsAsync((JobCandidate)null);

            // Act
            var result = await _service.UpsertJobCandidateAsync(candidateModel);

            // Assert
            Assert.Equal("New", result.FirstName);
            _mockRepo.Verify(repo => repo.AddAsync(It.Is<JobCandidate>(c =>
                c.FirstName == candidateModel.FirstName &&
                c.LastName == candidateModel.LastName &&
                c.Email == candidateModel.Email &&
                c.PhoneNumber == candidateModel.PhoneNumber &&
                c.PreferredCallTimeFrom == TimeSpan.Parse(candidateModel.PreferredCallTimeFrom) &&
                c.PreferredCallTimeTo == TimeSpan.Parse(candidateModel.PreferredCallTimeTo) &&
                c.LinkedInProfileUrl == candidateModel.LinkedInProfileUrl &&
                c.GitHubProfileUrl == candidateModel.GitHubProfileUrl &&
                c.FreeTextComment == candidateModel.Comment)), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Once);
        }
    }
}
