using JobCandidateHub.DataAccess.Models;
using JobCandidateHub.DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.DataAccess.Repositories
{
    public interface IJobCandidateRepository : IBaseRepository<JobCandidate>
    {
        Task<JobCandidate> GetByEmailAsync(string email);
    }
}
