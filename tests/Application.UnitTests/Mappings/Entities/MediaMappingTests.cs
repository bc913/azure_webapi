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
    public class MediaMappingTests
    {
        private IConfigurationProvider _configurationProvider { get; }
        private IMapper _mapper { get; }

        public MediaMappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MediaProfile>();
            });

            _mapper = _configurationProvider.CreateMapper();
        }

        [Fact]
        public void ConfigurationShouldBeValid()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingToDtoShouldSucceed()
        {
            // Given
            var entity = FakeMedia.ImageInstance;
            // When
            var dto = _mapper.Map<MediaDto>(entity);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<Media>(
                entity,
                options => options.ComparingByMembers<Media>()
                                  .ComparingByMembers<MediaResolution>());
        }

        [Fact]
        public void MappingFromDtoShouldSucceed()
        {
            // Given
            var dto = new MediaDto
            {
                Id = Guid.NewGuid(),
                Type = MediaTypeDto.Image,
                Original = new MediaResolutionDto
                {
                    Url = "Some Url",
                    Height = 34,
                    Width = 45
                },
                Thumbnail = new MediaResolutionDto
                {
                    Url = "some other url",
                    Height = 23,
                    Width = 32
                }
            };
            // When
            var entity = _mapper.Map<Media>(dto);
            // Then
            entity.Should().NotBeNull();
            entity.Should().BeEquivalentTo<MediaDto>(dto);
        }

        [Fact]
        public void MappingFromDtoShouldFailForUndefinedMediaType()
        {
            // Given
            var dto = new MediaDto
            {
                Id = Guid.NewGuid(),
                Type = MediaTypeDto.Undefined,
                Original = new MediaResolutionDto
                {
                    Url = "Some Url",
                    Height = 34,
                    Width = 45
                },
                Thumbnail = new MediaResolutionDto
                {
                    Url = "some other url",
                    Height = 23,
                    Width = 32
                }
            };
            // When
            Action act = () => _mapper.Map<Media>(dto);
            // Then
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void MappingFromDtoShouldFailForNullOriginal()
        {
            // Given
            var dto = new MediaDto
            {
                Id = Guid.NewGuid(),
                Type = MediaTypeDto.Video,
                Thumbnail = new MediaResolutionDto
                {
                    Url = "some other url",
                    Height = 23,
                    Width = 32
                }
            };
            // When
            Action act = () => _mapper.Map<Media>(dto);
            // Then
            act.Should().Throw<Exception>();
        }
    }
}