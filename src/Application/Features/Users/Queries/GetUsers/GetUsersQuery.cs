using MediatR;
using System.Collections.Generic;

namespace Bcan.Backend.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<List<UserLiteDto>>
    {
        
    }
}