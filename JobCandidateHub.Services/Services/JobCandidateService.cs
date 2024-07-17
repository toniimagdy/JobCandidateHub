using JobCandidateHub.DataAccess.Models;
using JobCandidateHub.DataAccess.Repositories;
using JobCandidateHub.DataAccess.UnitOfWork;
using JobCandidateHub.Models;
using JobCandidateHub.Services.IServices;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Services.Services
{
    public class JobCandidateService : IJobCandidateService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IJobCandidateRepository _jobCandidateRepository;
        private const string CacheKeyPrefix = "JobCandidate_";

        public JobCandidateService(IUnitOfWorkAsync unitOfWork,
                                    IMemoryCache cache,
                                    IJobCandidateRepository jobCandidateRepository)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _jobCandidateRepository = jobCandidateRepository;
        }

        public async Task<JobCandidateModel> UpsertJobCandidateAsync(JobCandidateModel jobCandidate)
        {
            var cacheKey = $"{CacheKeyPrefix}{jobCandidate.Email}";
            if (!_cache.TryGetValue(cacheKey, out JobCandidate existingCandidate))
            {
                existingCandidate = await this._jobCandidateRepository.GetByEmailAsync(jobCandidate.Email);
                if (existingCandidate != null)
                {
                    _cache.Set(cacheKey, existingCandidate, TimeSpan.FromMinutes(5));
                }
            }

            if (existingCandidate != null)
            {
                existingCandidate.FirstName = jobCandidate.FirstName;
                existingCandidate.LastName = jobCandidate.LastName;
                existingCandidate.PhoneNumber = jobCandidate.PhoneNumber;
                existingCandidate.PreferredCallTimeFrom = TimeSpan.Parse(jobCandidate.PreferredCallTimeFrom);
                existingCandidate.PreferredCallTimeTo = TimeSpan.Parse(jobCandidate.PreferredCallTimeTo);
                existingCandidate.LinkedInProfileUrl = jobCandidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = jobCandidate.GitHubProfileUrl;
                existingCandidate.FreeTextComment = jobCandidate.Comment;

                await _jobCandidateRepository.UpdateAsync(existingCandidate);
            }
            else
            {
                var entity = new JobCandidate
                {
                    FirstName = jobCandidate.FirstName,
                    LastName = jobCandidate.LastName,
                    PhoneNumber = jobCandidate.PhoneNumber,
                    Email = jobCandidate.Email,
                    PreferredCallTimeFrom = TimeSpan.Parse(jobCandidate.PreferredCallTimeFrom),
                    PreferredCallTimeTo = TimeSpan.Parse(jobCandidate.PreferredCallTimeTo),
                    LinkedInProfileUrl = jobCandidate.LinkedInProfileUrl,
                    GitHubProfileUrl = jobCandidate.GitHubProfileUrl,
                    FreeTextComment = jobCandidate.Comment
                };
                await _jobCandidateRepository.AddAsync(entity);
                _cache.Set(cacheKey, entity, TimeSpan.FromMinutes(5));
            }

            await _unitOfWork.CommitAsync();
            return jobCandidate;
        }
    }
}
