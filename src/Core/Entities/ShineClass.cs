using Ardalis.GuardClauses;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.SharedKernel;
using Bcan.Backend.SharedKernel.Contracts;
using System;

namespace Bcan.Backend.Core.Entities
{
    public class ShineClass : BaseEntity<Guid>, IAggregateRootWithId<Guid>
    {
        private ShineClass() : base(Guid.NewGuid()) { }

        public ShineClass(Guid id, string title, DanceInfo info,
        Location location, DateTimeOffsetRange time, EventPolicy policy,
        Fee fee, string description, Media media = null) : base(id)
        {
            Title = Guard.Against.NullOrWhiteSpace(title, nameof(title), "Dance class should have a title.");
            Info = Guard.Against.Null<DanceInfo>(info, nameof(info), "DanceInfo instance can not be null for a dance class.");
            Location = Guard.Against.Null<Location>(location, nameof(location), "Location should be defined for a dance class.");
            Time = Guard.Against.Null<DateTimeOffsetRange>(time, nameof(time), "Class should have start and end time defined.");
            Policy = Guard.Against.Null<EventPolicy>(policy, nameof(policy), "Class should have an event policy defined.");
            Fee = Guard.Against.Null<Fee>(fee, nameof(fee), "Class definition requires a fee policy to be defined.");
            Description = description;
            Media = media;
        }

        public string Title { get; private set; }
        public ShineEventType Type { get; private set; } = ShineEventType.Class;
        public DanceInfo Info { get; private set; }

        public Location Location { get; private set; }
        public DateTimeOffsetRange Time { get; private set; }

        public EventPolicy Policy { get; private set; }
        public Fee Fee { get; private set; }

        public string Description { get; private set; }
        public Media Media { get; private set; }

    }
}