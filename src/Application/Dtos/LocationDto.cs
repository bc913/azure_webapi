using System;

namespace Bcan.Backend.Application.Dtos
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public AddressDto Address { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class LocationLiteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}