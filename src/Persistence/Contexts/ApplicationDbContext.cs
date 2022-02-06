using Bcan.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bcan.Backend.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
            
        }

        public DbSet<ShineEvent> Events { get; set; }
        public DbSet<ShineClass> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // As per Microsoft docs, base implementation does nothing.
            //base.OnModelCreating(modelBuilder);
        }
    }
}