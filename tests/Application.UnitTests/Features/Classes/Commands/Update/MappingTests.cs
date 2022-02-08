using AutoMapper;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Features.Classes.Commands.Update;
using Bcan.Backend.Core.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Bcan.Backend.TestHelpers.FakeGenerators;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Features.Classes.Commands.Update
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        private UpdateClassCommand _command;

        public MappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UpdateClassProfile>();
            });
            _mapper = _configurationProvider.CreateMapper();
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            var info = new DanceInfoDto
            {
                Level = DanceLevelDto.Advanced,
                Types = new List<DanceTypeDto> { DanceTypeDto.Salsa, DanceTypeDto.Bachata }
            };
            var policy = new EventPolicyDto { DressCode = true, DanceShoes = true, Partner = false };

            IReadOnlyCollection<FeeOptionDto> options = new List<FeeOptionDto> 
            {
                new FeeOptionDto { Value = 90.0m, Individual = IndividualTypeDto.Student, Payment = PaymentTypeDto.OneTime, Description = "student desc" }, 
                new FeeOptionDto { Value = 180.0m, Individual = IndividualTypeDto.Regular, Payment = PaymentTypeDto.Financed, Description = "regular desc" }
            };
            var fee = new FeeDto { Options = options, Description = "some fee dto desc" };

            _command = new UpdateClassCommand
            {
                Id = Guid.NewGuid(),
                Title = "New Latin dances class",
                Info = info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = fee,
                Policy = policy,
                Description = "Some class description here"
            };
        }

        [Fact]
        public void MappingConfigurationShouldBeValid()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingToEntityShouldSucceed()
        {
            // Given

            // When
            var entity = _mapper.Map<ShineClass>(_command);
            // Then
            entity.Should().NotBeNull();
            entity.Should().BeEquivalentTo<UpdateClassCommand>(_command,
                options => options.ExcludingMissingMembers().Excluding(c => c.Id)); //TODO: Base Entity class does not have backing field
                // for id so reflection skips for automapper.
            entity.Time.Start.Should().Be(_command.Start);
            entity.Time.End.Should().Be(_command.End);
        }

        [Fact]
        public void MappingToEntityShouldFailForNullTitle()
        {
            // Given
            _command.Title = null;
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForEmptyTitle()
        {
            // Given
            _command.Title = string.Empty;
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullDanceInfo()
        {
            // Given
            _command.Info = null;
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullLocation()
        {
            // Given
            _command.Location = null;
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForInvalidDate()
        {
            // Given
            _command.Start = new DateTimeOffset(DateTime.UtcNow).AddDays(13);
            _command.End = new DateTimeOffset(DateTime.UtcNow).AddDays(12);
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullFee()
        {
            // Given
            _command.Fee = null;
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullFeeOptions()
        {
            // Given
            _command.Fee.Options = null;
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForEmptyFeeOptions()
        {
            // Given
            _command.Fee.Options = new List<FeeOptionDto>();
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }

        [Fact]
        public void MappingToEntityShouldFailForNullPolicy()
        {
            // Given
            _command.Policy = null;
            // When
            Action act = () => _mapper.Map<ShineClass>(_command);
            // Then (Type of exception is not important since we only care about the failure)
            act.Should().Throw<AutoMapperMappingException>();
        }
    }
}