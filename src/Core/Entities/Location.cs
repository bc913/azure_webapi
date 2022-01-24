using Ardalis.GuardClauses;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.SharedKernel;
using System;

namespace Bcan.Backend.Core.Entities
{
    public class Location : BaseEntity<Guid>
    {
        private Location() : base(Guid.NewGuid()){}
        
        public Location(Guid id, Address address, string name, double latitude, double longitude) : 
            base(id)
        {
            Address = Guard.Against.Null<Address>(address, nameof(address));
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Latitude = Guard.Against.OutOfRange<double>(latitude, nameof(latitude), -90.0, 90.0);
            Longitude = Guard.Against.OutOfRange<double>(longitude, nameof(longitude), -180.0, 180.0);
        }

        public Address Address { get; private set; }
        public string Name { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public override string ToString()
        {
            return $"Location: {Name} [Lat: {Latitude} - Lon: {Longitude}]";
        }
    }
}