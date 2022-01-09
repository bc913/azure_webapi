using Bcan.Backend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bcan.Backend.Persistence.IntegrationTests
{
    internal sealed class TestContext : ApplicationDbContext
    {
        public TestContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        public override void Dispose()
        {
            Database.CloseConnection();
            base.Dispose();
        }
    }
}