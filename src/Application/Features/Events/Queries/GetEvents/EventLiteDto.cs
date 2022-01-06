using System;

namespace Bcan.Backend.Application.Features.Events.Queries.GetEvents
{
    public class ShineEventLiteDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }
    }
}