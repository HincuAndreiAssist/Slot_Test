using Microsoft.EntityFrameworkCore;
using Slot_Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Slot_Test.Repositories.Database
{
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Session> Session { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

        }
    }
}
