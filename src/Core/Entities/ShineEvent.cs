using Ardalis.GuardClauses;
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
        public ShineEvent(Guid id, string title, ShineEventType eventType) : base(id)
        {
            Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Type = eventType;
        }

        public string Title { get; private set; }
        public ShineEventType Type { get; private set; }
    }
}