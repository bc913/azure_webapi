using Bcan.Backend.Application.Features.Events.Queries.GetEvents;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.TestHelpers.FakeGenerators;
using System;
using AutoMapper;
using Xunit;
using FluentAssertions;


namespace Bcan.Backend.Application.UnitTests.Features.Events.Queries.GetEvents
{
    public class MappingTests
    {
        public MappingTests() {}

        [Fact]
        public void ConfigurationShouldBeValid()
        {
            //Given

            //When
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ShineEventLiteProfile>());
            //Then
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingFromEntityToDtoShouldSucceed()
        {
            //Given
            var theEvent = new ShineEvent(Guid.NewGuid(), "Pittsburgh Socials", ShineEventType.Class, FakeLocation.Instance);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ShineEventLiteProfile>());
            var mapper = config.CreateMapper();
            //When
            var dto = mapper.Map<ShineEventLiteDto>(theEvent);
            //Then
            dto.Should().NotBeNull();
            dto.Id.Should().Be(theEvent.Id);
            dto.Title.Should().Be(theEvent.Title);
            dto.Type.Should().Be(theEvent.Type.ToString());
        }
    }
}