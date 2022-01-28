using Bcan.Backend.Core.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.ValueObjects
{
    public class DanceInfoUnitTests
    {
        [Fact]
        public void CtorShouldSucceedForValidArgs()
        {
            var level = DanceLevel.Beginner;
            var types = new List<DanceType> { DanceType.Bachata };

            var sut = new DanceInfo(level, types);

            sut.Level.Should().Be(level);
            sut.Types.Should().BeEquivalentTo(types);
        }

        [Fact]
        public void CtorShouldThrowForNullTypes()
        {
            Action act = () => new DanceInfo(DanceLevel.Advanced, null);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForEmptyTypes()
        {
            Action act = () => new DanceInfo(DanceLevel.Beginner, new List<DanceType>());
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void EqualityShouldSucceedForSameValuedInstances()
        {
            var types1 = new List<DanceType> { DanceType.Bachata, DanceType.ChaCha };
            var types2 = new List<DanceType> { DanceType.Bachata, DanceType.ChaCha };

            var sut1 = new DanceInfo(DanceLevel.Beginner, types1);
            var sut2 = new DanceInfo(DanceLevel.Beginner, types2);

            sut1.Should().Be(sut2);
            sut2.Should().Be(sut1);
            (sut1 == sut2).Should().BeTrue();
            (sut1 != sut2).Should().BeFalse();
        }

        [Fact]
        public void EqualityShouldFailForDifferentValuedInstances()
        {
            var sut1 = new DanceInfo(DanceLevel.Advanced, new List<DanceType> { DanceType.Bachata });
            var sut2 = new DanceInfo(DanceLevel.Advanced, new List<DanceType> { DanceType.Salsa });

            sut1.Should().NotBe(sut2);
            sut2.Should().NotBe(sut1);
            (sut1 == sut2).Should().BeFalse();
            (sut1 != sut2).Should().BeTrue();
        }
    }
}