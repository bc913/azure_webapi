using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Bcan.Backend.Application.Features.Events.Queries.GetEvents;
using Microsoft.AspNetCore.Authorization;

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
    }
}