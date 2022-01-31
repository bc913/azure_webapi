using System;

namespace Bcan.Backend.Application.Dtos
{
    public enum MediaTypeDto
    {
        Undefined,
        Image,
        Video
    }

    public class MediaDto
    {
        public Guid Id { get; set; }
        public MediaTypeDto Type { get; set; }
        public MediaResolutionDto Original { get; set; }
        public MediaResolutionDto Thumbnail { get; set; }
    }
}