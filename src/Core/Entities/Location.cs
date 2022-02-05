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
            Address = address;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        #region Fields - Properties

        private Address _address;
        public Address Address 
        {
            get => _address;
            private set => _address = Guard.Against.Null<Address>(value, nameof(Address));
        }

        private string _name;
        public string Name 
        {
            get => _name;
            private set => _name = Guard.Against.NullOrWhiteSpace(value, nameof(Name));
        }

        private double _latitude;
        public double Latitude 
        {
            get => _latitude;
            private set => _latitude =  Guard.Against.OutOfRange<double>(value, nameof(Latitude), -90.0, 90.0);
        }

        private double _longitude;
        public double Longitude 
        {
            get => _longitude;
            private set => _longitude = Guard.Against.OutOfRange<double>(value, nameof(Longitude), -180.0, 180.0);
        }

        #endregion

        public override string ToString()
        {
            return $"Location: {Name} [Lat: {Latitude} - Lon: {Longitude}]";
        }
    }
}