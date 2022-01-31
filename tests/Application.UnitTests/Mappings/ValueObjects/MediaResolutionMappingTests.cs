using AutoMapper;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Profiles;
using FluentAssertions;
using System;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Mappings.ValueObjects
{
    public class MediaResolutionMappingTests
    {
        public MediaResolutionMappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MediaResolutionProfile>();
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
        public void MappingToDtoShouldSucceed()
        {
            // Given
            var vo = new MediaResolution("someUrl", 34, 56);
            // When
            var dto = _mapper.Map<MediaResolutionDto>(vo);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<MediaResolution>(vo, options => options.ComparingByMembers<MediaResolution>());
        }

        [Fact]
        public void MappingFromDtoShouldSucceed()
        {
            // Given
            var dto = new MediaResolutionDto { Url = "someUrl", Height = 34, Width = 565 };
            // When
            var vo = _mapper.Map<MediaResolution>(dto);
            // Then
            vo.Should().NotBeNull();
            vo.Should().BeEquivalentTo<MediaResolutionDto>(dto);
        }

        [Fact]
        public void MappingFromDtoShouldFailForEmptyUrl()
        {
            // Given
            var dto = new MediaResolutionDto { Url = "", Height = 34, Width = 565 };
            // When
            Action act = () => _mapper.Map<MediaResolution>(dto);
            // Then
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void MappingFromDtoShouldFailForNullUrl()
        {
            // Given
            var dto = new MediaResolutionDto { Url = null, Height = 34, Width = 565 };
            // When
            Action act = () => _mapper.Map<MediaResolution>(dto);
            // Then
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void MappingFromDtoShouldFailForInvalidHeight()
        {
            // Given
            var dto = new MediaResolutionDto { Url = "someUrl", Height = -34, Width = 565 };
            // When
            Action act = () => _mapper.Map<MediaResolution>(dto);
            // Then
            act.Should().Throw<Exception>();
        }
        
        [Fact]
        public void MappingFromDtoShouldFailForInvalidWidth()
        {
            // Given
            var dto = new MediaResolutionDto { Url = "someUrl", Height = 34, Width = -565 };
            // When
            Action act = () => _mapper.Map<MediaResolution>(dto);
            // Then
            act.Should().Throw<Exception>();
        }
    }
}