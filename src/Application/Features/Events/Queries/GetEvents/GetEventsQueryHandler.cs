using AutoMapper;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Contracts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bcan.Backend.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IReadOnlyCollection<ShineEventLiteDto>>
    {

        private readonly IReadRepository<ShineEvent> _repository;
        private readonly IMapper _mapper;

        public GetEventsQueryHandler(IMapper mapper, IReadRepository<ShineEvent> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<ShineEventLiteDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            // var allEvents = await _repository.ListAsync(cancellationToken);
            var allEvents = await Task.FromResult(new List<ShineEvent>()
            {
                new ShineEvent(Guid.NewGuid(), "Seviche Socials", ShineEventType.Social),
                new ShineEvent(Guid.NewGuid(), "Absolute Ballroom", ShineEventType.Festival),
                new ShineEvent(Guid.NewGuid(), "Beginner - Salsa", ShineEventType.Class)
            });

            return _mapper.Map<IReadOnlyCollection<ShineEventLiteDto>>(allEvents);
        }
    }
}