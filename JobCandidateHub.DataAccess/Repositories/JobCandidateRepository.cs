using JobCandidateHub.DataAccess.Contexts;
using JobCandidateHub.DataAccess.Models;
using JobCandidateHub.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.DataAccess.Repositories
{
    public class JobCandidateRepository : BaseRepository<JobCandidate>, IJobCandidateRepository
    {
        public JobCandidateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<JobCandidate> GetByEmailAsync(string email)
        {
            return await _context.Set<JobCandidate>().FirstOrDefaultAsync(jc => jc.Email == email);
        }
    }
}
