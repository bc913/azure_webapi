using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Bcan.Backend.Application.Features.Users.Queries.GetUsers;

namespace Bcan.Backend.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var result = await _mediator.Send(new GetUsersQuery());
            var allUsers = await Task.FromResult(new List<UserLiteDto>()
            {
                new UserLiteDto{ Id = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"), FullName = "Burak Can",   NickName = "bc913"},
                new UserLiteDto{ Id = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}"), FullName = "Eren Bekar",   NickName = "eb926"},
                new UserLiteDto{ Id = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}"), FullName = "Basar Ozkurt",  NickName = "bo916"}
            });
            return Ok(allUsers);
        }
    }
}