using MediatR;
using System;

namespace Bcan.Backend.Application.Features.Classes.Queries.ShineClassDetail
{
    public class ShineClassDetailQuery : IRequest<ShineClassDetailDto>
    {
        public Guid Id { get; set; }
    }
}