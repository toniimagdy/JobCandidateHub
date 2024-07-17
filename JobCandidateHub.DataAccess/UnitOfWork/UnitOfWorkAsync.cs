using JobCandidateHub.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.DataAccess.UnitOfWork
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private ApplicationDbContext _context;


        public UnitOfWorkAsync(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            var result = await this._context.SaveChangesAsync();
            return result;
        }    
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
