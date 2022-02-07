using MediatR;
using System;

namespace Bcan.Backend.Application.Features.Classes.Commands.Delete
{
    public class DeleteClassCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}