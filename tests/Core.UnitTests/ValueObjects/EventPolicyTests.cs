using Bcan.Backend.Core.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.ValueObjects
{
    public class EventPolicyTests
    {

        [Fact]
        public void CtorShouldSucceed()
        {
            var noDressCode = false;
            var partnerRequired = true;
            var danceShoesRequired = true;
            var description = "This is test event policy";

            var sut = new EventPolicy(noDressCode, partnerRequired, danceShoesRequired, description);

            sut.DressCode.Should().BeFalse();
            sut.Partner.Should().BeTrue();
            sut.DanceShoes.Should().BeTrue();
            sut.Description.Should().Be(description);
        }

        [Fact]
        public void NoPolicyFactoryMethodShouldSucceed()
        {
            var sut = EventPolicy.NoPolicy();

            sut.DressCode.Should().BeFalse();
            sut.Partner.Should().BeFalse();
            sut.DanceShoes.Should().BeFalse();
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void DressCodeRequiredFactoryMethodShouldSucceed()
        {
            var sut = EventPolicy.DressCodeRequired();

            sut.DressCode.Should().BeTrue();
            sut.Partner.Should().BeFalse();
            sut.DanceShoes.Should().BeFalse();
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void PartnerRequiredFactoryMethodShouldSucceed()
        {
            var sut = EventPolicy.PartnerRequired();

            sut.DressCode.Should().BeFalse();
            sut.Partner.Should().BeTrue();
            sut.DanceShoes.Should().BeFalse();
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void DressCodeAndDanceShoesRequiredFactoryMethodShouldSucceed()
        {
            var sut = EventPolicy.DressCodeAndDanceShoesRequired();

            sut.DressCode.Should().BeTrue();
            sut.Partner.Should().BeFalse();
            sut.DanceShoes.Should().BeTrue();
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void DressCodeAndPartnerRequiredFactoryMethodShouldSucceed()
        {
            var sut = EventPolicy.DressCodeAndPartnerRequired();

            sut.DressCode.Should().BeTrue();
            sut.Partner.Should().BeTrue();
            sut.DanceShoes.Should().BeFalse();
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void AllRequiredFactoryMethodShouldSucceed()
        {
            var sut = EventPolicy.AllRequired();

            sut.DressCode.Should().BeTrue();
            sut.Partner.Should().BeTrue();
            sut.DanceShoes.Should().BeTrue();
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void EqualityIgnoresDescription()
        {
            var sut1 = EventPolicy.DressCodeAndDanceShoesRequired();
            var sut2 = EventPolicy.DressCodeAndDanceShoesRequired("Some description");

            sut1.Should().Be(sut2);
            sut2.Should().Be(sut1);
            (sut1 == sut2).Should().BeTrue();
            (sut1 != sut2).Should().BeFalse();
        }

        [Fact]
        public void NotEqualsForDifferentInstances()
        {
            var sut1 = EventPolicy.AllRequired();
            var sut2 = EventPolicy.DressCodeAndDanceShoesRequired();

            sut1.Should().NotBe(sut2);
            sut2.Should().NotBe(sut1);
            (sut1 == sut2).Should().BeFalse();
            (sut1 != sut2).Should().BeTrue();
        }
    }
}