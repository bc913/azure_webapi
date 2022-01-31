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
    public class FeeMappingTestFixture
    {
        public FeeMappingTestFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FeeProfile>();
            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }
        public IMapper Mapper { get; }
    }

    public class FeeMappingTests : IClassFixture<FeeMappingTestFixture>
    {
        public FeeMappingTests(FeeMappingTestFixture fixture)
        {
            _configurationProvider = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
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
            var option1 = new FeeOption(90.0m, IndividualType.Regular, PaymentType.OneTime);
            var option2 = new FeeOption(45.0m, IndividualType.Student, PaymentType.OneTime);
            var options = new List<FeeOption> { option1, option2 };
            var description = "Some fee desc";
            var vo = new Fee(options, description);

            // When
            var dto = _mapper.Map<FeeDto>(vo);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<Fee>(vo, options => options.ComparingByMembers<Fee>().ComparingByMembers<FeeOption>());
        }

        [Fact]
        public void MappingFromDtoShouldSucceed()
        {
            // Given
            var option1 = new FeeOptionDto { Value = 90.0m, Individual = IndividualTypeDto.Student, Payment = PaymentTypeDto.OneTime, Description = "student desc" };
            var option2 = new FeeOptionDto { Value = 180.0m, Individual = IndividualTypeDto.Regular, Payment = PaymentTypeDto.Financed, Description = "regular desc" };
            var options = new List<FeeOptionDto> { option1, option2 };
            var description = "some fee dto desc";
            var dto = new FeeDto { Options = options, Description = description };
            // When
            var vo = _mapper.Map<Fee>(dto);
            // Then
            vo.Should().NotBeNull();
            vo.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public void MappingFromDtoForNullOptionsShouldFail()
        {
            // Given
            var dto = new FeeDto { Options = null };
            // When
            Action act = () => _mapper.Map<Fee>(dto);
            // Then
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void MappingFromDtoForEmptyOptionsShouldFail()
        {
            // Given
            var dto = new FeeDto { Options = new List<FeeOptionDto>() };
            // When
            Action act = () => _mapper.Map<Fee>(dto);
            // Then
            act.Should().Throw<Exception>();
        }
    }

    public class FeeOptionMappingTests : IClassFixture<FeeMappingTestFixture>
    {
        public FeeOptionMappingTests(FeeMappingTestFixture fixture)
        {
            _configurationProvider = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        private IConfigurationProvider _configurationProvider { get; }
        private IMapper _mapper { get; }
        
        [Fact]
        public void MappingFromValueObjectToDtoShouldSucceed()
        {
            // Given
            var description = "FeeOption desc";
            var vo = new FeeOption(90.0m, IndividualType.Regular, PaymentType.OneTime, description);
            // When
            var dto = _mapper.Map<FeeOptionDto>(vo);
            // Then
            dto.Should().NotBeNull();                   
            // Since FeeOption overrides Object.Equals and BeEquivalentTo uses Object.Equals
            // disable it by allowing memberwise comparison using ComparingByMembers on FeeOption
            dto.Should().BeEquivalentTo<FeeOption>(vo, options => options.ComparingByMembers<FeeOption>());
        }

        [Fact]
        public void MappingFromValueObjectToDtoShouldSucceedForNullDescription()
        {
            // Given
            var vo = new FeeOption(90.0m, IndividualType.Regular, PaymentType.OneTime);
            // When
            var dto = _mapper.Map<FeeOptionDto>(vo);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<FeeOption>(vo, options => options.ComparingByMembers<FeeOption>());
        }

        [Fact]
        public void MappingFromDtoToValueObjectShouldSucceed()
        {
            // Given
            var dto = new FeeOptionDto 
            { 
                Value = 90.0m, Individual = IndividualTypeDto.Student, 
                Payment = PaymentTypeDto.OneTime, Description = "Some description" 
            };
            // When
            var vo = _mapper.Map<FeeOption>(dto);
            // Then
            vo.Should().NotBeNull();
            vo.Should().BeEquivalentTo<FeeOptionDto>(dto);
        }

        [Fact]
        public void MappingFromDtoToValueObjectShouldSucceedForNullDescription()
        {
            // Given
            var dto = new FeeOptionDto 
            { 
                Value = 90.0m, 
                Individual = IndividualTypeDto.Student, 
                Payment = PaymentTypeDto.OneTime
            };
            // When
            var vo = _mapper.Map<FeeOption>(dto);
            // Then
            vo.Should().NotBeNull();
            vo.Should().BeEquivalentTo<FeeOptionDto>(dto);
        }
    }
}