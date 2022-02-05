using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;
using Bcan.Backend.Core.ValueObjects;
using System;

namespace Bcan.Backend.Core.Entities
{
    public enum MediaType
    {
        Undefined,
        Image,
        Video
    }

    public class Media : BaseEntity<Guid>
    {
        private Media() : base(Guid.NewGuid()) {}

        public Media(Guid id, MediaType type, MediaResolution original, MediaResolution thumbnail = null) :
            base(id)
        {
            Type = type;
            Original = original;
            Thumbnail = thumbnail;
        }

        private MediaType _type;
        public MediaType Type 
        {
            get => _type;
            private set => _type = value != MediaType.Undefined 
                            ? value 
                            : throw new ArgumentException("MediaType can not be Undefined.", nameof(Type));
        }

        private MediaResolution _original;
        public MediaResolution Original 
        {
            get => _original;
            private set => _original = Guard.Against.Null<MediaResolution>(value, nameof(Original));
        }

        public MediaResolution Thumbnail { get; private set; }
    }
}