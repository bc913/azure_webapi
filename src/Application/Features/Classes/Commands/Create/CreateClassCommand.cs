using Bcan.Backend.Application.Dtos;
using MediatR;
using System;

namespace Bcan.Backend.Application.Features.Classes.Commands.Create
{
    public class CreateClassCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public DanceInfoDto Info { get; set; }
        public LocationDto Location { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public EventPolicyDto Policy { get; set; }
        public FeeDto Fee { get; set; }
        public string Description { get; set; }
    }
}