using Bcan.Backend.SharedKernel.Contracts;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Persistence.Contexts;
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
    }
}