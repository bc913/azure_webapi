using MediatR;
using Bcan.Backend.Application.Dtos;
using System;

namespace Bcan.Backend.Application.Features.Classes.Commands.Update
{
    public class UpdateClassCommand : IRequest
    {
        public Guid Id { get; set; }
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