using AutoMapper;
using Bcan.Backend.Core;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Profiles;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Mappings.ValueObjects
{
    public class DanceInfoMappingTests
    {
        public DanceInfoMappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DanceInfoProfile>();
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
            var vo = new DanceInfo(DanceLevel.Advanced, new List<DanceType> { DanceType.ChaCha, DanceType.Salsa });
            // When
            var dto = _mapper.Map<DanceInfoDto>(vo);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<DanceInfo>(vo, options => options.ComparingByMembers<DanceInfo>().WithoutStrictOrdering());
        }

        [Fact]
        public void MappingFromDtoShouldSucceed()
        {
            // Given
            var dto = new DanceInfoDto { Level = DanceLevelDto.Advanced, Types = new List<DanceTypeDto> { DanceTypeDto.Salsa, DanceTypeDto.Bachata } };
            // When
            var vo = _mapper.Map<DanceInfo>(dto);
            // Then
            vo.Should().NotBeNull();
            vo.Should().BeEquivalentTo<DanceInfoDto>(dto);
        }

        [Fact]
        public void MappingFromDtoShouldFailForNullTypes()
        {
            // Given
            var dto = new DanceInfoDto { Level = DanceLevelDto.Beginner, Types = null };
            // When
            Action act = () => _mapper.Map<DanceInfo>(dto);
            // Then
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void MappingFromDtoShouldFailForEmptyTypes()
        {
            // Given
            var dto = new DanceInfoDto { Level = DanceLevelDto.Beginner, Types = new List<DanceTypeDto>() };
            // When
            Action act = () => _mapper.Map<DanceInfo>(dto);
            // Then
            act.Should().Throw<Exception>();
        }
    }
}