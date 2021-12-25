using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bcan.Backend.SharedKernel.Contracts;
using Bcan.Backend.Application.Contracts.Repositories;


namespace Bcan.Backend.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
    {
        public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}