using Ainthinai.Service.DomainRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.DependencyInjection
{
    public static class RepositoryServiceCollectionExtensions
    {
        //public static IServiceCollection AddRepository<TContext1, TContext2>(this IServiceCollection services)
        //    where TContext1 : DbContext
        //    where TContext2 : DbContext
        //{
        //    services.AddScoped<IEventRepository<TContext1>, EventRepository<TContext1>>();
        //    //services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
        //    return services;

        //}

        public static IServiceCollection AddRepository<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IEventRepository, EventRepository<TContext>>();
            services.AddScoped<IEventRepository<TContext>, EventRepository<TContext>>();
            return services;

        }
    }
}
