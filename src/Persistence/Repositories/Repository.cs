using Bcan.Backend.SharedKernel.Contracts;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bcan.Backend.Persistence.Repositories
{
    public class Repository<T> : ReadRepository<T>, IRepository<T> where T : class, IAggregateRootWithId<Guid>
    {

        public Repository(ApplicationDbContext context) : base(context)
        {
            
        }
        public async Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task DeleteAsync (T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            // _context.Set<T>().Update(entity);
            // remove the line above if you gonna keep the one below
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}