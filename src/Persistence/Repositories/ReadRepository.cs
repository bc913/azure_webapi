using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel.Contracts;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bcan.Backend.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IAggregateRootWithId<Guid>
    {

        protected readonly ApplicationDbContext _context;

        public ReadRepository(ApplicationDbContext context)
        {
            _context = Guard.Against.Null<ApplicationDbContext>(context, nameof(context));
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }
    }
}