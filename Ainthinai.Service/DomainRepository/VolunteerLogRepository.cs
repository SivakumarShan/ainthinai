using Ainthinai.Service.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.DomainRepository
{
    public class VolunteerLogRepository<TContext> : IVolunteerLogRepository, IVolunteerLogRepository<TContext> where TContext : DbContext
    {
        public VolunteerLogRepository(TContext context)
        {

        }
        public TContext Context => throw new NotImplementedException();

        public Task<bool> AddVolunteerLog(VolunteerLog logs)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateVolunteerLog(int id, VolunteerLog log)
        {
            throw new NotImplementedException();
        }
    }
}
