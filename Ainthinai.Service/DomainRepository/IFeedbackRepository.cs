using System;
using System.Collections.Generic;
using System.Linq;
using Ainthinai.Service.Model;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ainthinai.Service.DomainRepository
{
    public interface IFeedbackRepository
    {
        Task<bool> PostFeedback(Feedback feedback);
        Task<IEnumerable<Feedback>> GetFeedbacks();
    }

    public interface IFeedbackRepository<TContext> : IFeedbackRepository where TContext : DbContext
    {
        TContext Context { get; }
    }
}
