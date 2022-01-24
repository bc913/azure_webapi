using Ardalis.GuardClauses;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.SharedKernel;
using Bcan.Backend.SharedKernel.Contracts;
using System;

namespace Bcan.Backend.Core.Entities
{
    public enum ShineEventType
    {
        Undefined,
        Class,
        Workshop,
        Social,
        Festival
    }

    public class ShineEvent : BaseEntity<Guid>, IAggregateRootWithId<Guid>
    {
        private ShineEvent() : base(Guid.NewGuid()){}
        
        public ShineEvent(Guid id, string title, ShineEventType type, Location location, Media media = null) : base(id)
        {
            if(type == ShineEventType.Undefined)
                throw new ArgumentException("A shine event should have an event type.", nameof(type));

            Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Type = type;
            Location = Guard.Against.Null<Location>(location, nameof(location));
            Media = media;
        }

        public string Title { get; private set; }
        public ShineEventType Type { get; private set; }
        public Location Location { get; private set; }
        public Media Media { get; private set; }
    }
}