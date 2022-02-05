using Ardalis.GuardClauses;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.SharedKernel;
using Bcan.Backend.SharedKernel.Contracts;
using System;

namespace Bcan.Backend.Core.Entities
{
    public class ShineClass : BaseEntity<Guid>, IAggregateRootWithId<Guid>
    {
        /*
        I use backing fields to maintain invariants during mapping process using AutoMapper.
        AutoMapper uses reflection and skips the parameter ctor so invariants are broken.
        Therefore, I'm using the Guards at property-field level to maintain the invariants
        */
        private ShineClass() : base(Guid.NewGuid()) { }

        public ShineClass(Guid id, string title, DanceInfo info,
            Location location, DateTimeOffsetRange time, EventPolicy policy,
            Fee fee, string description = null, Media media = null) : base(id)
        {
            Title = title;
            Info = info;
            Location = location;
            Time = time;
            Policy = policy;
            Fee = fee;
            Description = description;
            Media = media;
        }

        public ShineEventType Type { get; private set; } = ShineEventType.Class;

        private string _title;
        public string Title
        {
            get => _title;
            private set => _title = Guard.Against.NullOrWhiteSpace(value, nameof(Title), "Dance class should have a title.");
        }

        private DanceInfo _info;
        public DanceInfo Info 
        {
            get => _info;
            private set => _info = Guard.Against.Null<DanceInfo>(value, nameof(Info), "DanceInfo instance can not be null for a dance class.");
        }

        private Location _location;
        public Location Location 
        {
            get => _location;
            private set => _location = Guard.Against.Null<Location>(value, nameof(Location), "Location should be defined for a dance class.");
        }

        private DateTimeOffsetRange _time;
        public DateTimeOffsetRange Time 
        {
            get => _time;
            private set => _time = Guard.Against.Null<DateTimeOffsetRange>(value, nameof(Time), "Class should have start and end time defined.");
        }

        private EventPolicy _policy;
        public EventPolicy Policy 
        {
            get => _policy;
            private set => _policy = Guard.Against.Null<EventPolicy>(value, nameof(Policy), "Class should have an event policy defined.");
        }

        private Fee _fee;
        public Fee Fee 
        {
            get => _fee;
            private set => _fee = Guard.Against.Null<Fee>(value, nameof(Fee), "Class definition requires a fee policy to be defined.");
        }

        private string _description;
        public string Description 
        {
            get => _description;
            private set => _description = value;
        }

        private Media _media;
        public Media Media 
        {
            get => _media;
            private set => _media = value;
        }
    }
}