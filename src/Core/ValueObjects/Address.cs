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
            Street = Guard.Against.NullOrWhiteSpace(street, nameof(street));
            City = Guard.Against.NullOrWhiteSpace(city, nameof(city));
            State = Guard.Against.NullOrWhiteSpace(state, nameof(state));
            Country = Guard.Against.NullOrWhiteSpace(country, nameof(country));
            ZipCode = Guard.Against.NullOrWhiteSpace(zipCode, nameof(zipCode));
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

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