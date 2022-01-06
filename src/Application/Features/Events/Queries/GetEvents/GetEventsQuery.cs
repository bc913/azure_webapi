using MediatR;
using System.Collections.Generic;

namespace Bcan.Backend.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQuery : IRequest<IReadOnlyCollection<ShineEventLiteDto>>
    {
        
    }
}