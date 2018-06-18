using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ainthinai.Service.Model;
using Ainthinai.Service.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ainthinai.Service.DomainRepository
{
    public class EventRepository<TContext> : IEventRepository<TContext>, IEventRepository where TContext : DbContext
    {
        private readonly IRepository<Event> _eventRepo;

        public EventRepository(TContext context)
        {
            Context = context;
            _eventRepo = new Repository<Event>(Context);
        }

        public TContext Context { get; }

        public async Task<Event> CreateEvent(Event @event)
        {
            await _eventRepo.Add(@event);
            return await Task.FromResult<Event>(@event);
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            var existingData = Context.Find<Event>(eventId);
            if (existingData != null)
            {
                await _eventRepo.Delete(existingData);
                return true;
            }
            return false;
        }

        public async Task<Event> GetEvent(int eventId)
        {
            return await _eventRepo.Get(eventId);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _eventRepo.Get();
        }

        public async Task<bool> UpdateEvent(int eventId, Event @event)
        {
            if (eventId == @event.Id)
            {
                Context.Entry<Event>(@event).Property(x => x.CreatedOn).IsModified = false;
                Context.Entry<Event>(@event).Property(x => x.CreatedBy).IsModified = false;
                @event.Id = eventId;
                await _eventRepo.Update(@event);
                return true;
            }
            return false;
        }
    }
}
