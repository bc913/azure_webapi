using AutoMapper;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bcan.Backend.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {

        private readonly IRepository<ShineEvent> _repository;
        private readonly IMapper _mapper;
        public CreateEventCommandHandler(IRepository<ShineEvent> repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var theEvent = _mapper.Map<ShineEvent>(request);
            return await _repository.AddAsync(theEvent, cancellationToken);
        }
    }
}