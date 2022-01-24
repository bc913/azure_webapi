using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using System;

namespace Bcan.Backend.TestHelpers.FakeGenerators
{
    public static class FakeMedia
    {
        private static readonly MediaResolution _original = new MediaResolution("urlToOriginal", 345, 120);
        private static readonly MediaResolution _thumbnail = new MediaResolution("urlToThumbnail", 120, 34);
        private static readonly Media _videoInstance = new Media(Guid.NewGuid(), MediaType.Video, _original, _thumbnail);
        private static readonly Media _imageInstance = new Media(Guid.NewGuid(), MediaType.Image, _original, _thumbnail);

        public static Media ImageInstance => _imageInstance;
        public static Media VideoInstance => _videoInstance;
    }
}