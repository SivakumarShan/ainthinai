using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ainthinai.Service.Model;
using Ainthinai.Service.Models;
using Ainthinai.Service.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ainthinai.Service.DomainRepository
{
    public class EventRepository : IEventRepository
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly DataContext _dataContext;

        public EventRepository(DataContext context, IRepository<Event> eventRepo)
        {
            _eventRepo = eventRepo;
            _dataContext = context;
        }

        public async Task<Event> CreateEvent(Event @event)
        {
            var result = await _dataContext.Event.AddAsync(@event);
            await _dataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            var @event = await _dataContext.Event.SingleOrDefaultAsync(m => m.Id == eventId);
            if (@event == null)
                return false;
            _dataContext.Event.Remove(@event);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Event> GetEvent(int eventId)
        {
            return await _dataContext.Event.SingleOrDefaultAsync(m => m.Id == eventId);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _dataContext.Event.ToListAsync();
        }

        public async Task<bool> UpdateEvent(int eventId, Event @event)
        {
             _dataContext.Entry(@event).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(eventId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool EventExists(int id)
        {
            return _dataContext.Event.Any(e => e.Id == id);
        }
    }
}
