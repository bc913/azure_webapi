using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using System;

namespace Bcan.Backend.TestHelpers.FakeGenerators
{
    public static class FakeLocation
    {
        private static readonly Address _address = new Address("455 Fobes Ave", "New York City", "NY", "US", "11102");
        private static readonly Location _instance = new Location(Guid.NewGuid(), _address, "Perle", 40.4411026, -80.0031469);
        public static Location Instance => _instance;
    }
}