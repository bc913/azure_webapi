using AutoMapper;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Profiles;
using FluentAssertions;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Mappings.ValueObjects
{
    public class EventPolicyMappingTests
    {
        public EventPolicyMappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EventPolicyProfile>();
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
            var vo = EventPolicy.DressCodeAndDanceShoesRequired("some desc");
            // When
            var dto = _mapper.Map<EventPolicyDto>(vo);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<EventPolicy>(vo, options => options.ComparingByMembers<EventPolicy>());
        }

        [Fact]
        public void MappingToDtoWithNullDescriptionShouldSucceed()
        {
            // Given
            var vo = EventPolicy.DressCodeAndPartnerRequired();
            // When
            var dto = _mapper.Map<EventPolicyDto>(vo);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<EventPolicy>(vo, options => options.ComparingByMembers<EventPolicy>());
        }

        [Fact]
        public void MappingFromDtoShouldSucceed()
        {
            // Given
            var dto = new EventPolicyDto 
            { 
                DressCode = false, DanceShoes = true, 
                Partner = true, Description = "some desc" 
            };
            // When
            var vo = _mapper.Map<EventPolicy>(dto);
            // Then
            vo.Should().NotBeNull();
            vo.Should().BeEquivalentTo<EventPolicyDto>(dto);
        }

        [Fact]
        public void MappingFromDtoWithNullDescriptionShouldSucceed()
        {
            // Given
            var dto = new EventPolicyDto 
            { 
                DressCode = false, DanceShoes = true, 
                Partner = true, Description = null
            };
            // When
            var vo = _mapper.Map<EventPolicy>(dto);
            // Then
            vo.Should().NotBeNull();
            vo.Should().BeEquivalentTo<EventPolicyDto>(dto);
        }
    }
}