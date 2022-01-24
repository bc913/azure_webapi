using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.Entities
{
    public class LocationTests
    {
        private readonly Address _address = new Address("455 Fobes Ave", "New York City", "NY", "US", "11102");

        [Fact]
        public void CtorShouldSucceedForValidArgs()
        {
            var id = Guid.NewGuid();
            var name = "Perle";
            var lat = 40.4411026;
            var lon = -80.0031469;

            var sut = new Location(id, _address, name, lat, lon);

            sut.Id.Should().Be(id);
            sut.Name.Should().Be(name);
            sut.Address.Should().Be(_address);
            sut.Latitude.Should().Be(lat);
            sut.Longitude.Should().Be(lon);
        }

        [Fact]
        public void CtorShouldThrowForNullAddress()
        {
            Action act = () => new Location(Guid.NewGuid(), null, "Perle", 34, 45);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForEmptyName()
        {
            Action act = () => new Location(Guid.NewGuid(), _address, "", 34.5, 43.5);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CtorShouldThrowForNullName()
        {
            Action act = () => new Location(Guid.NewGuid(), _address, null, 34.5, 43.5);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void LatitudeShouldNotBeOutOfRange()
        {
            var id = Guid.NewGuid();

            Action act = () => new Location(id, _address, "Perle", -93.0, 34.9);
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();

            act = () => new Location(id, _address, "Perle", 120.0, 34.9);
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void LongitudeShouldNotBeOutOfRange()
        {
            var id = Guid.NewGuid();

            Action act = () => new Location(id, _address, "Perle", 45.0, -181.0);
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();

            act = () => new Location(id, _address, "Perle", 45.0, 181.0);
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }
    }
}
