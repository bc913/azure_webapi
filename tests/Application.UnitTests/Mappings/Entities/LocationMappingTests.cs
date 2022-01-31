using AutoMapper;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Profiles;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.TestHelpers.FakeGenerators;
using FluentAssertions;
using System;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Mappings.Entities
{
    public class LocationMappingTests
    {
        private IConfigurationProvider _configurationProvider { get; }
        private IMapper _mapper { get; }

        public LocationMappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LocationProfile>();
            });

            _mapper = _configurationProvider.CreateMapper();
        }

        [Fact]
        public void MappingToDtoShouldSucceed()
        {
            // Given
            var entity = FakeLocation.Instance;
            // When
            var dto = _mapper.Map<LocationDto>(entity);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<Location>(
                entity, 
                options => options.ComparingByMembers<Location>()
                                  .ComparingByMembers<Address>());
        }

        [Fact]
        public void MappingFromDtoShouldSucceed()
        {
            // Given
            var dto = new LocationDto
            {
                Id = Guid.NewGuid(),
                Address = new AddressDto { Street = "Forbes", City = "NYC", State = "NY", Country = "US", ZipCode = "11102" },
                Name = "SomeLocation",
                Latitude = 34.4342,
                Longitude = 45.3234
            };
            // When
            var entity = _mapper.Map<Location>(dto);
            // Then
            entity.Should().NotBeNull();
            entity.Should().BeEquivalentTo<LocationDto>(dto);
        }

        [Fact]
        public void MappingFromDtoShouldFailForNullAddress()
        {
            // Given
            var dto = new LocationDto { Id = Guid.NewGuid(), Name = "SomeLocation", Latitude = 34.453, Longitude = 54.42 };
            // When
            Action act = () => _mapper.Map<Location>(dto);
            // Then
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void MappingFromDtoShouldFailForInvalidLatitude()
        {
            // Given
            var dto = new LocationDto
            {
                Id = Guid.NewGuid(),
                Address = new AddressDto { Street = "Forbes", City = "NYC", State = "NY", Country = "US", ZipCode = "11102" },
                Name = "SomeLocation",
                Latitude = -1834.4342,
                Longitude = 45.3234
            };
            // When
            Action act = () => _mapper.Map<Location>(dto);
            // Then
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void MappingFromDtoShouldFailForInvalidLongitude()
        {
            // Given
            var dto = new LocationDto
            {
                Id = Guid.NewGuid(),
                Address = new AddressDto { Street = "Forbes", City = "NYC", State = "NY", Country = "US", ZipCode = "11102" },
                Name = "SomeLocation",
                Latitude = -1834.4342,
                Longitude = 45.3234
            };
            // When
            Action act = () => _mapper.Map<Location>(dto);
            // Then
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void MappingToLiteDtoShouldSucceed()
        {
            // Given
            var entity = FakeLocation.Instance;
            // When
            var dto = _mapper.Map<LocationLiteDto>(entity);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<Location>(entity, options =>
                options.ComparingByMembers<Location>()
                    .ComparingByMembers<Address>()
                    .ExcludingMissingMembers()); // Only include the members both classes have
        }
    }
}