using AutoMapper;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Application.Exceptions;
using Bcan.Backend.Core.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bcan.Backend.Application.Features.Classes.Commands.Update
{
    public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand>
    {
        private readonly IRepository<ShineClass> _repository;
        private readonly IMapper _mapper;

        public UpdateClassCommandHandler(IRepository<ShineClass> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var classToUpdate = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if(classToUpdate is null)
                throw new NotFoundException(nameof(ShineClass), request.Id);

            var validator = new UpdateClassCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if(validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, classToUpdate);
            await _repository.UpdateAsync(classToUpdate, cancellationToken);

            return Unit.Value;
        }
    }
}