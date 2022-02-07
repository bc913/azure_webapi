using AutoMapper;
using Bcan.Backend.Application.Features.Classes.Queries.ShineClassDetail;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.TestHelpers.Assert;
using Bcan.Backend.TestHelpers.FakeGenerators;
using FluentAssertions;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Features.Classes.Queries.ShineClassDetail
{
    public class MappingTests
    {
        private IConfigurationProvider _configurationProvider;
        private IMapper _mapper;

        public MappingTests()
        {
            _configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShineClassDetailProfile>();
            });

            _mapper = _configurationProvider.CreateMapper();
        }

        [Fact]
        public void ProfileConfigurationShouldBeValid()
        {
            _configurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingFromEntityShouldSucceed()
        {
            // Given
            var entity = FakeShineClass.Instance;
            // When
            var dto = _mapper.Map<ShineClassDetailDto>(entity);
            // Then
            dto.Should().NotBeNull();
            dto.Should().BeEquivalentTo<ShineClass>(entity, 
                options => options.ComparingByMembers<ShineClass>()
                                    .ComparingByMembers<DanceInfo>()
                                    .ComparingByMembers<Location>()
                                    .ComparingByMembers<Address>()
                                    .ComparingByMembers<EventPolicy>()
                                    .ComparingByMembers<Fee>()
                                    .ComparingByMembers<FeeOption>()
                                    .ComparingByMembers<MediaResolution>()
                                    .ComparingByMembers<Media>()
                                    .ExcludingMissingMembers());
            
            dto.Start.Should().Be(entity.Time.Start);
            dto.End.Should().Be(entity.Time.End);
        }
    }
}