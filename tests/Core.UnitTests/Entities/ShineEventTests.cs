using Bcan.Backend.Core.Entities;
using Bcan.Backend.TestHelpers.FakeGenerators;
using System;
using FluentAssertions;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.Entities
{
    public class ShineEventTests
    {
        [Fact]
        public void CtorShouldNotThrowForValidArgs()
        {
            var id = Guid.NewGuid();
            var title = "Pittsburgh Socials";
            var danceType = ShineEventType.Social;
            var loc = FakeLocation.Instance;

            var theEvent = new ShineEvent(id, title, danceType, loc);

            theEvent.Id.Should().Be(id);
            theEvent.Title.Should().Be(title);
            theEvent.Type.Should().Be(danceType);
            theEvent.Location.Should().Be(loc);
            theEvent.Media.Should().BeNull();
        }

        [Fact]
        public void CtorShouldNotThrowForValidArgsWithMedia()
        {
            var id = Guid.NewGuid();
            var title = "Pittsburgh Socials";
            var danceType = ShineEventType.Social;
            var loc = FakeLocation.Instance;
            var media = FakeMedia.VideoInstance;

            var theEvent = new ShineEvent(id, title, danceType, loc, media);

            theEvent.Id.Should().Be(id);
            theEvent.Title.Should().Be(title);
            theEvent.Type.Should().Be(danceType);
            theEvent.Location.Should().Be(loc);
            theEvent.Media.Should().Be(media);
        }

        [Fact]
        public void CtorShouldThrowForUndefinedEventType()
        {
            Action act = () => new ShineEvent(Guid.NewGuid(), "Perle Thursday socails", ShineEventType.Undefined, FakeLocation.Instance);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CtorShouldThrowForEmptyTitle()
        {
            Action act = () => new ShineEvent(Guid.NewGuid(), "", ShineEventType.Festival, FakeLocation.Instance);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CtorShouldThrowForNullTitle()
        {
            Action act = () => new ShineEvent(Guid.NewGuid(), null, ShineEventType.Workshop, FakeLocation.Instance);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForNullLocation()
        {
            Action act = () => new ShineEvent(Guid.NewGuid(), "Perle Thursday socials", ShineEventType.Social, null);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void InstancesWithDiffIdShouldNotBeEqual()
        {
            var title = "Some title";
            var eventType = ShineEventType.Festival;
            var event_1 = new ShineEvent(Guid.NewGuid(), title, eventType, FakeLocation.Instance);
            var event_2 = new ShineEvent(Guid.NewGuid(), title, eventType, FakeLocation.Instance);

            event_1.Should().NotBe(event_2);
            (event_1 == event_2).Should().BeFalse();
        }
    }
}