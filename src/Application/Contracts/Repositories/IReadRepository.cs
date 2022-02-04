using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bcan.Backend.SharedKernel.Contracts;

namespace Bcan.Backend.Application.Contracts.Repositories
{
    public interface IReadRepository<T> where T : class, IAggregateRoot
    {
        Task<IReadOnlyCollection<T>> ListAsync(CancellationToken cancellationToken);

        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}