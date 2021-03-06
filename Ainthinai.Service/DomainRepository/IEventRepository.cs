﻿using Ainthinai.Service.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.DomainRepository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEvent(int eventId);
        Task<Event> CreateEvent(Event @event);
        Task<bool> UpdateEvent(int eventId, Event @event);
        Task<bool> DeleteEvent(int eventId);
    }

    public interface IEventRepository<TContext> : IEventRepository where TContext : DbContext
    {
        TContext Context { get; }
    }
}
