using Bcan.Backend.Application.Features.Events.Queries.GetEvents;
using Bcan.Backend.Application.Features.Events.Commands.CreateEvent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Threading.Tasks;
namespace Bcan.Backend.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(scopeRequiredByApi)]
    public class EventsController : ControllerBase
    {
        internal const string scopeRequiredByApi = "access_as_user";
        private readonly IMediator _mediator;
        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetEventsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventCommand createEventCommand)
        {
            var id = await _mediator.Send(createEventCommand);
            return Ok(id);
        }
    }
}