using Bcan.Backend.Persistence.Repositories;
using Bcan.Backend.Persistence.Contexts;
using Bcan.Backend.SharedKernel.Contracts;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bcan.Backend.Persistence.IntegrationTests.Repositories
{
    public abstract class RepositoryTestFixture
    {
        private ApplicationDbContext _context;

        public RepositoryTestFixture()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlite("Filename=:memory:");
            _context = new TestContext(builder.Options);
        }

        protected Repository<T> GetRepository<T>() where T : class, IAggregateRootWithId<Guid>
        {
            return new Repository<T>(_context);
        }
    }
}