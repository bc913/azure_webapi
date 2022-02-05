using System.Collections.Generic;
using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;

namespace Bcan.Backend.Core.ValueObjects
{
    public class Address : ValueObject
    {
        private Address() {}

        public Address(string street, string city, string state, string country, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        private string _street;
        public string Street 
        {
            get => _street;
            private set => _street = Guard.Against.NullOrWhiteSpace(value, nameof(Street));
        }

        private string _city;
        public string City 
        { 
            get => _city;
            private set => _city = Guard.Against.NullOrWhiteSpace(value, nameof(City));
        }

        private string _state;
        public string State 
        {
            get => _state;
            private set => _state = Guard.Against.NullOrWhiteSpace(value, nameof(value));
        }

        private string _country;
        public string Country 
        {
            get => _country;
            private set => _country = Guard.Against.NullOrWhiteSpace(value, nameof(value));
        }

        private string _zipCode;
        public string ZipCode 
        {
            get => _zipCode;
            private set => _zipCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}