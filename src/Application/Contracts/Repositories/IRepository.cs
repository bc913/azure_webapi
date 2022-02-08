using System;
using System.Threading;
using System.Threading.Tasks;
using Bcan.Backend.SharedKernel.Contracts;

namespace Bcan.Backend.Application.Contracts.Repositories
{
    public interface IRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
    {
        // return id or newly created entity
        Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}