using Ainthinai.Service.Model;
using Ainthinai.Service.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.DomainRepository
{
    public class FeedbackRepository<TContext> : IFeedbackRepository<TContext>, IFeedbackRepository where TContext : DbContext
    {
        private readonly IRepository<Feedback> _feedbackRepo;
        public FeedbackRepository(TContext context)
        {
            _feedbackRepo = new Repository<Feedback>(context);
            Context = context;
        }

        public TContext Context { get; }

        public async Task<bool> PostFeedback(Feedback feedback)
        {
            await _feedbackRepo.Add(feedback);
            return true;
        }

        public async Task<IEnumerable<Feedback>> GetFeedbacks()
        {
            return await _feedbackRepo.Get();
        }
    }
}
