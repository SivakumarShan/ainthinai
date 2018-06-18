using Ainthinai.Service.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.DomainRepository
{
    public interface IVolunteerLogRepository
    {
        Task<bool> AddVolunteerLog(VolunteerLog logs);
        Task<bool> UpdateVolunteerLog(int id, VolunteerLog log);
    }

    public interface IVolunteerLogRepository<TContext> : IVolunteerLogRepository where TContext : DbContext
    {
        TContext Context { get; }
    }
}
