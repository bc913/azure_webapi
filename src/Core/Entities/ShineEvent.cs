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

            Title = title;
            Type = type;
            Location = location;
            Media = media;
        }

        private string _title;
        public string Title 
        { 
            get => _title;
            private set => _title = Guard.Against.NullOrWhiteSpace(value, nameof(Title));
        }

        private ShineEventType _type;
        public ShineEventType Type 
        {
            get => _type;
            private set => _type = value;
        }

        private Location _location;
        public Location Location 
        {
            get => _location;
            private set => _location = Guard.Against.Null<Location>(value, nameof(Location));
        }

        private Media _media;
        public Media Media 
        {
            get => _media;
            private set => _media = value;
        }
    }
}