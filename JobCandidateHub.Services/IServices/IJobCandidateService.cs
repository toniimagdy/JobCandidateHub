using JobCandidateHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Services.IServices
{
    public interface IJobCandidateService
    {
        Task<JobCandidateModel> UpsertJobCandidateAsync(JobCandidateModel jobCandidate);
    }
}
