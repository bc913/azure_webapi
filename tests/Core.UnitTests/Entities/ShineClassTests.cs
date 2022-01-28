using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.SharedKernel;
using Bcan.Backend.TestHelpers.FakeGenerators;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.Entities
{
    public class ShineClassTests
    {
        private readonly DanceInfo _danceInfo = new DanceInfo(
            DanceLevel.Advanced, 
            new List<DanceType> { DanceType.Bachata, DanceType.Salsa, DanceType.ChaCha });

        private readonly Location _location = FakeLocation.Instance;
        private readonly DateTimeOffsetRange _time = DateTimeOffsetRange.CreateOneDayRange(DateTimeOffset.UtcNow);
        private readonly EventPolicy _policy = EventPolicy.DressCodeAndDanceShoesRequired();
        private readonly Fee _fee = Fee.FreeForStudents();

        [Fact]
        public void CtorShouldSucceedWithValidArgs()
        {
            var title = "Test shine class";

            var sut = new ShineClass(Guid.NewGuid(), title, _danceInfo, _location, _time, _policy, _fee);

            sut.Title.Should().Be(title);
            sut.Info.Should().Be(_danceInfo);
            sut.Location.Should().Be(_location);
            sut.Time.Should().Be(_time);
            sut.Policy.Should().Be(_policy);
            sut.Fee.Should().Be(_fee);
            sut.Description.Should().BeNull();
            sut.Media.Should().BeNull();
        }

        [Fact]
        public void DescriptionCanHaveValue()
        {
            var title = "Test shine class";
            var description = "Some description";

            var sut = new ShineClass(Guid.NewGuid(), title, _danceInfo, _location, _time, _policy, _fee, description);

            sut.Title.Should().Be(title);
            sut.Info.Should().Be(_danceInfo);
            sut.Location.Should().Be(_location);
            sut.Time.Should().Be(_time);
            sut.Policy.Should().Be(_policy);
            sut.Fee.Should().Be(_fee);
            sut.Description.Should().Be(description);
            sut.Media.Should().BeNull();
        }

        [Fact]
        public void MediaCanHaveValue()
        {
            var title = "Test shine class";
            var media = new Media(Guid.NewGuid(), MediaType.Video, new MediaResolution("some_url", 345, 45));

            var sut = new ShineClass(Guid.NewGuid(), title, _danceInfo, _location, _time, _policy, _fee, null, media);

            sut.Title.Should().Be(title);
            sut.Info.Should().Be(_danceInfo);
            sut.Location.Should().Be(_location);
            sut.Time.Should().Be(_time);
            sut.Policy.Should().Be(_policy);
            sut.Fee.Should().Be(_fee);
            sut.Description.Should().BeNull();
            sut.Media.Should().Be(media);
        }

        [Fact]
        public void CtorShouldThrowForNullTitle()
        {
            Action act = () => new ShineClass(Guid.NewGuid(), null, _danceInfo, _location, _time, _policy, _fee);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForEmptyTitle()
        {
            Action act = () => new ShineClass(Guid.NewGuid(), "", _danceInfo, _location, _time, _policy, _fee);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CtorShouldThrowForNullDanceInfo()
        {
            Action act = () => new ShineClass(Guid.NewGuid(), "some title", null, _location, _time, _policy, _fee);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForNullLocation()
        {
            Action act = () => new ShineClass(Guid.NewGuid(), "some title", _danceInfo, null, _time, _policy, _fee);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForNullTime()
        {
            Action act = () => new ShineClass(Guid.NewGuid(), "some title", _danceInfo, _location, null, _policy, _fee);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForNullEventPolicy()
        {
            Action act = () => new ShineClass(Guid.NewGuid(), "some title", _danceInfo, _location, _time, null, _fee);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForNullFeePolicy()
        {
            Action act = () => new ShineClass(Guid.NewGuid(), "some title", _danceInfo, _location, _time, _policy, null);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

    }
}