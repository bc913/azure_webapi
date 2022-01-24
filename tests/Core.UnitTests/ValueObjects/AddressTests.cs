using Bcan.Backend.Core.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.ValueObjects
{
    public class AddressTests
    {
        private readonly string _street = "3456 Shady Ave";
        private readonly string _city = "New York City";
        private readonly string _state = "NY";
        private readonly string _country = "US";
        private readonly string _zipCode = "12345";

        [Fact]
        public void CtorShouldSucceedWithValidArgs()
        {
            var sut = new Address(_street, _city, _state, _country, _zipCode);

            sut.Street.Should().Be(_street);
            sut.City.Should().Be(_city);
            sut.State.Should().Be(_state);
            sut.Country.Should().Be(_country);
            sut.ZipCode.Should().Be(_zipCode);
        }

        [Fact]
        public void CtorShouldThrowForInvalidStreetValue()
        {
            Action act = () => new Address("", _city, _state, _country, _zipCode);
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(" ", _city, _state, _country, _zipCode);
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(null, _city, _state, _country, _zipCode);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForInvalidCityValue()
        {
            Action act = () => new Address(_street, "", _state, _country, _zipCode);
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(_street, " ", _state, _country, _zipCode);
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(_street, null, _state, _country, _zipCode);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForInvalidStateValue()
        {
            Action act = () => new Address(_street, _city, "", _country, _zipCode);
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(_street, _city, " ", _country, _zipCode);
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(_street, _city, null, _country, _zipCode);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForInvalidCountryValue()
        {
            Action act = () => new Address(_street, _city, _state, "", _zipCode);
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(_street, _city, _state, " ", _zipCode);
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(_street, _city, _state, null, _zipCode);
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void CtorShouldThrowForInvalidZipCodeValue()
        {
            Action act = () => new Address(_street, _city, _state, _country, "");
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(_street, _city, _state, _country, "  ");
            act.Should().ThrowExactly<ArgumentException>();

            act = () => new Address(_street, _city, _state, _country, null);
            act.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}