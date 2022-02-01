using AutoMapper;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Core.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bcan.Backend.Application.Features.Classes.Commands.Create
{
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Guid>
    {
        private readonly IRepository<ShineClass> _repository;
        private readonly IMapper _mapper;
        public CreateClassCommandHandler(IRepository<ShineClass> repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            var classEvent = _mapper.Map<ShineClass>(request);
            return await _repository.AddAsync(classEvent, cancellationToken);
        }
    }
}