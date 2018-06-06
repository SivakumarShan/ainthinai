using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Ainthinai.Service.Models;
using Ainthinai.Service.Model;
using Ainthinai.Service.EnumObjects;

namespace Ainthinai.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddDbContext<DataContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("DataContext")));
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Ainthinai"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var context = app.ApplicationServices.GetService<DataContext>();
            SeedData(context);
            app.UseMvc();
        }

        private static void SeedData(DataContext context)
        {
            var events = new Event
            {
                Id = 1,
                Name = "CTC Ainthinai - Nursery Maintenance Drive",
                Place = "Sholinganallur",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now,
                Description = "Nursery Maintenance Drive",
                VolunteersCount = 10,
                Organizers = "Siva",
                ContactDetails = "9894069761"
            };
            context.Event.Add(events);

            var taskList = new TaskList
            {
                Id = 1,
                EventType = EventType.Nursery,
                TaskName = "Soil Refilling",
                TaskDetails = "Soil has to be filled in water staganted area",
                TaskStatus = EnumObjects.TaskStatus.New,
                Remarks = "Work in progress"
            };

            context.TaskList.Add(taskList);

            context.SaveChanges();
        }
    }
}
