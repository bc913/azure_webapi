using Bcan.Backend.Core.Entities;
using Bcan.Backend.Application.Exceptions;
using Bcan.Backend.Application.Contracts.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bcan.Backend.Application.Features.Classes.Commands.Delete
{
    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand>
    {
        private readonly IRepository<ShineClass> _repository;

        public DeleteClassCommandHandler(IRepository<ShineClass> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var classToDelete = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if(classToDelete is null)
                throw new NotFoundException(nameof(ShineClass), request.Id);

            await _repository.DeleteAsync(classToDelete, cancellationToken);
            return Unit.Value;
        }
    }
}