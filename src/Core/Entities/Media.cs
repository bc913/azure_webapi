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
            if(type == MediaType.Undefined)
                throw new ArgumentException("MediaType can not be Undefined.", nameof(type));

            Type = type;
            Original = Guard.Against.Null<MediaResolution>(original, nameof(original));
            Thumbnail = thumbnail;
        }

        public MediaType Type { get; private set; }

        public MediaResolution Original { get; private set; }

        public MediaResolution Thumbnail { get; private set; }
    }
}