using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.Entities
{
    public class MediaTests
    {
        private readonly MediaResolution _thumbnail = new MediaResolution("someURl", 34, 45);
        private readonly MediaResolution _original = new MediaResolution("originalUrl", 456, 567);

        [Fact]
        public void ThumbnailCanBeNull()
        {
            var mediaType = MediaType.Video;

            var sut = new Media(Guid.NewGuid(), mediaType, _original);

            sut.Type.Should().Be(mediaType);
            sut.Original.Should().Be(_original);
            sut.Thumbnail.Should().BeNull();
        }

        [Fact]
        public void CtorShouldSucceedWithAllValidArgs()
        {
            var mediaType = MediaType.Image;

            var sut = new Media(Guid.NewGuid(), mediaType, _original, _thumbnail);

            sut.Type.Should().Be(mediaType);
            sut.Original.Should().Be(_original);
            sut.Thumbnail.Should().Be(_thumbnail);
        }

        [Fact]
        public void OriginalMediaResolutionCanNotBeNull()
        {
            Action act = () => new Media(Guid.NewGuid(), MediaType.Image, null);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowIfMediaTypeIsUndefined()
        {
            Action act = () => new Media(Guid.NewGuid(), MediaType.Undefined, new MediaResolution("url", 34, 45));
            act.Should().ThrowExactly<ArgumentException>();
        }
    }
}