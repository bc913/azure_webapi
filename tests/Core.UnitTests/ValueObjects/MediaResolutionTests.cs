using Bcan.Backend.Core.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.ValueObjects
{
    public class MediaResolutionTests
    {
        private string _sampleUrl = "https://shineapp.blob.core.windows.net/files/Events/social.jpg";

        [Fact]
        public void CtorShouldSucceedWithValidArgs()
        {
            var url = "https://shineapp.blob.core.windows.net/files/Events/social.jpg";
            var height = 34;
            var width = 56;

            var sut = new MediaResolution(url, height, width);

            sut.Url.Should().Be(url);
            sut.Height.Should().Be(height);
            sut.Width.Should().Be(width);
        }

        [Fact]
        public void CtorShouldThrowForEmptyUrl()
        {
            Action act = () => new MediaResolution("", 34, 45);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CtorShouldThrowForNullUrl()
        {
            Action act = () => new MediaResolution(null, 34, 45);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForNegativeOrZeroHeight()
        {
            Action act = () => new MediaResolution(_sampleUrl, 0, 45);
            act.Should().ThrowExactly<ArgumentException>("Given height is zero.");

            act = () => new MediaResolution(_sampleUrl, -34, 45);
            act.Should().ThrowExactly<ArgumentException>("Given height has negative value.");
        }

        [Fact]
        public void CtorShouldThrowForNegativeOrZeroWidth()
        {
            Action act = () => new MediaResolution(_sampleUrl, 34, 0);
            act.Should().ThrowExactly<ArgumentException>("Given width is zero");

            act = () => new MediaResolution(_sampleUrl, 34, -45);
            act.Should().ThrowExactly<ArgumentException>("Given width has negative value.");
        }

        [Fact]
        public void FactoryMethodWithChangedResolutionShouldSucceed()
        {
            var source = new MediaResolution(_sampleUrl, 45, 63);

            var height = 452;
            var width = 563;
            var sut = source.NewWithChangedResolution(height, width);

            sut.Should().NotBe(source);
            sut.Height.Should().Be(height);
            sut.Width.Should().Be(width);
        }

        [Fact]
        public void FactoryMethodWithChangedUrlShouldSucceed()
        {
            var height = 452;
            var width = 563;
            var source = new MediaResolution(_sampleUrl, height, width);

            var url = "https://shine.com";
            var sut = source.NewWithChangedUrl(url);

            sut.Should().NotBe(source);
            sut.Url.Should().Be(url);
            sut.Height.Should().Be(height);
            sut.Width.Should().Be(width);
        }

        [Fact]
        public void SameValuedInstancesShouldBeEqual()
        {
            var height = 345;
            var width = 563;

            var mr1 = new MediaResolution(_sampleUrl, height, width);
            var mr2 = new MediaResolution(_sampleUrl, height, width);

            mr1.Should().Be(mr2);
            mr2.Should().Be(mr1);
            (mr1 == mr2).Should().BeTrue();
        }

        [Fact]
        public void DifferentValuedInstancesShouldNotBeEqual()
        {
            var height = 345;
            var width = 563;

            var mr1 = new MediaResolution(_sampleUrl, height, width);
            var mr2 = new MediaResolution(_sampleUrl, height, 45);

            mr1.Should().NotBe(mr2);
            mr2.Should().NotBe(mr1);
            (mr1 == mr2).Should().BeFalse();
            (mr1 != mr2).Should().BeTrue();
        }
    }
}