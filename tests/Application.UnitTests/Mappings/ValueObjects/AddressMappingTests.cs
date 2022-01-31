using AutoMapper;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Profiles;
using FluentAssertions;
using System;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Mappings.ValueObjects
{
    public class AddressMappingTests
    {
        public AddressMappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AddressProfile>();
            });

            _mapper = _configurationProvider.CreateMapper();
        }

        private IConfigurationProvider _configurationProvider { get; }
        private IMapper _mapper { get; }

        [Fact]
        public void ConfigurationShouldBeValid()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingFromValueObjectToDtoShouldSucceed()
        {
            // Given
            var vo = new Address("Some street", "New York City", "NY", "US", "15217");
            // When
            var dto = _mapper.Map<AddressDto>(vo);
            // Then
            dto.Should().NotBeNull();
            dto.Street.Should().Be(vo.Street);
            dto.City.Should().Be(vo.City);
            dto.State.Should().Be(vo.State);
            dto.Country.Should().Be(vo.Country);
            dto.ZipCode.Should().Be(vo.ZipCode);
        }

        [Fact]
        public void MappingFromDtoToValueObjectShouldSucceed()
        {
            // Given
            var dto = new AddressDto
            {
                Street = "Some street",
                City = "New York City",
                State = "NY",
                Country = "US",
                ZipCode = "15217"
            };
            // When
            var vo = _mapper.Map<Address>(dto);
            // Then
            vo.Should().NotBeNull();
            vo.Street.Should().Be(dto.Street);
            vo.City.Should().Be(dto.City);
            vo.State.Should().Be(dto.State);
            vo.Country.Should().Be(dto.Country);
            vo.ZipCode.Should().Be(dto.ZipCode);
        }

        [Fact]
        public void MappingFromDtoToValueObjectShouldFailForNullProps()
        {
            // Given
            var dto = new AddressDto
            {
                Street = "Some street",
                State = "NY",
                ZipCode = "15217"
            };
            // When
            Action act = () => _mapper.Map<Address>(dto);
            // Then
            act.Should().Throw<Exception>();
        }
    }
}