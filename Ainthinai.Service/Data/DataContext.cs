using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ainthinai.Service.Model;

namespace Ainthinai.Service
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Event { get; set; }
        public DbSet<TaskList> TaskList { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Ainthinai.Service.Model.VolunteerLog> VolunteerLog { get; set; }
    }
}
