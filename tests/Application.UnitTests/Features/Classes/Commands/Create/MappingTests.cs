using AutoMapper;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Features.Classes.Commands.Create;
using Bcan.Backend.Core.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Bcan.Backend.TestHelpers.FakeGenerators;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Features.Classes.Commands.Create
{
    public class MappingTests
    {
        private IConfigurationProvider _configurationProvider { get; }
        private IMapper _mapper { get; }

        #region Shared Data
        private DanceInfoDto _info;
        private EventPolicyDto _policy;
        private FeeDto _fee;
        #endregion

        public MappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateClassProfile>();
            });

            _mapper = _configurationProvider.CreateMapper();

            _info = new DanceInfoDto
            {
                Level = DanceLevelDto.Advanced,
                Types = new List<DanceTypeDto> { DanceTypeDto.Salsa, DanceTypeDto.Bachata }
            };
            _policy = new EventPolicyDto { DressCode = true, DanceShoes = true, Partner = false };

            IReadOnlyCollection<FeeOptionDto> options = new List<FeeOptionDto> 
            {
                new FeeOptionDto { Value = 90.0m, Individual = IndividualTypeDto.Student, Payment = PaymentTypeDto.OneTime, Description = "student desc" }, 
                new FeeOptionDto { Value = 180.0m, Individual = IndividualTypeDto.Regular, Payment = PaymentTypeDto.Financed, Description = "regular desc" }
            };
            _fee = new FeeDto { Options = options, Description = "some fee dto desc" };
        }

        [Fact]
        public void ConfigurationShouldBeValid()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingToEntityShouldSucceed()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = "New Latin dances class",
                Info = _info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = _fee,
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            var entity = _mapper.Map<ShineClass>(command);
            // Then
            // TODO: Use WithMapping to compare dto.Start and dto.End with entity.Time.Start and entity.Time.End
            entity.Should().NotBeNull();
            entity.Should().BeEquivalentTo<CreateClassCommand>(command, 
                options => options.ExcludingMissingMembers() // Expectation (command) instance has some properties that subject instance does not have so ignore
            );
        }

        [Fact]
        public void MappingToEntityShouldFailForNullTitle()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = null,
                Info = _info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = _fee,
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForEmptyTitle()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = string.Empty,
                Info = _info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = _fee,
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullDanceInfo()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = "Some title",
                Info = null,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = _fee,
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullLocation()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = "Some title",
                Info = _info,
                Location = null,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = _fee,
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForInvalidDate()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = "Some title",
                Info = _info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(13), //Start date is later than end date
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                Fee = _fee,
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullFee()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = "Some title",
                Info = _info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = null,
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullFeeOptions()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = "Some title",
                Info = _info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = new FeeDto {Options = null},
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForEmptyFeeOptions()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = "Some title",
                Info = _info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = new FeeDto {Options = null},
                Policy = _policy,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullPolicy()
        {
            // Given
            var command = new CreateClassCommand
            {
                Title = "Some title",
                Info = _info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = _fee,
                Policy = null,
                Description = "Some class description here"
            };
            // When
            Action act = () => _mapper.Map<ShineClass>(command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }
    }
}