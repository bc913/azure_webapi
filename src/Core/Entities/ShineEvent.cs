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

        public static ShineEventType ConvertTypeFromString(string eventType)
        {
            Guard.Against.NullOrWhiteSpace(eventType, nameof(eventType));
            switch (eventType)
            {
                case "Workshop":
                    return ShineEventType.Workshop;                
                case "Class":
                    return ShineEventType.Class;
                case "Social":
                    return ShineEventType.Social;
                case "Festival":
                    return ShineEventType.Festival;
                default:
                    return ShineEventType.Undefined;
            }
        }

        public static string ConvertTypeToString(ShineEventType eventType)
        {
            switch (eventType)
            {
                case ShineEventType.Class:
                    return "Class";
                case ShineEventType.Festival:
                    return "Festival";
                case ShineEventType.Social:
                    return "Social";
                case ShineEventType.Workshop:
                    return "Workshop";
                default:
                    return "Undefined";
            }
        }
    }
}