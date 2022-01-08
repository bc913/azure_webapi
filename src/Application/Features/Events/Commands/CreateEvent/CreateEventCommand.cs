using MediatR;
using System;

namespace Bcan.Backend.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public string Title { get; set; }

        public string EventType { get; set; }

        
    }
}