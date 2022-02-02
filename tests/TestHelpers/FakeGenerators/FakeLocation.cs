using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;
using System;

namespace Bcan.Backend.TestHelpers.FakeGenerators
{
    public static class FakeLocation
    {
        private static readonly Address _address = new Address("455 Fobes Ave", "New York City", "NY", "US", "11102");
        private static readonly Location _instance = new Location(Guid.NewGuid(), _address, "Perle", 40.4411026, -80.0031469);
        public static Location Instance => _instance;
    }

    public static class FakeLocationDto
    {
        private static readonly AddressDto _address = new AddressDto
        {
            Street = "455 Fobes Ave",
            City = "New York City",
            State = "NY",
            Country = "US",
            ZipCode = "11102"
        };

        private static readonly LocationDto _instance = new LocationDto
        {
            Id = Guid.NewGuid(),
            Address = _address,
            Name = "Perle",
            Longitude = 40.4411026,
            Latitude = -80.0031469
        };
        public static LocationDto Instance => _instance;
    }
}