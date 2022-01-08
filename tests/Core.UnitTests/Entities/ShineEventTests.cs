using System;
using FluentAssertions;
using Xunit;

using Bcan.Backend.Core.Entities;

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

            var theEvent = new ShineEvent(id, title, danceType);

            theEvent.Id.Should().Be(id);
            theEvent.Title.Should().Be(title);
            theEvent.Type.Should().Be(danceType);
        }

        [Fact]
        public void CtorShouldThrowForEmptyTitle()
        {
            Action act = () => new ShineEvent(Guid.NewGuid(), "", ShineEventType.Festival);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CtorShouldThrowForNullTitle()
        {
            Action act = () => new ShineEvent(Guid.NewGuid(), null, ShineEventType.Workshop);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void InstancesWithDiffIdShouldNotBeEqual()
        {
            var title = "Some title";
            var eventType = ShineEventType.Festival;
            var event_1 = new ShineEvent(Guid.NewGuid(), title, eventType);
            var event_2 = new ShineEvent(Guid.NewGuid(), title, eventType);

            event_1.Should().NotBe(event_2);
            (event_1 == event_2).Should().BeFalse();
        }
    }
}