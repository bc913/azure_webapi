using Bcan.Backend.Core.Entities;
using Bcan.Backend.Persistence.Repositories;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Bcan.Backend.TestHelpers.Assert;
using Bcan.Backend.TestHelpers.FakeGenerators;
using Xunit;

namespace Bcan.Backend.Persistence.IntegrationTests.Repositories.EventRepositoryTests
{
    public class GetList : RepositoryTestFixture
    {
        private readonly Repository<ShineEvent> _sut;

        public GetList()
        {
            _sut = GetRepository<ShineEvent>();
        }

        [Fact]
        public async Task GetAllEventsShouldSucceed()
        {
            var addedEvent = await AddAsync();
            
            var events = await _sut.ListAsync();
            var actual = events.First();

            actual.Should().BeValueEqual(addedEvent);
        }

        private async Task<ShineEvent> AddAsync()
        {
            var eventToAdd = new ShineEvent(Guid.NewGuid(), "Some title", ShineEventType.Social, FakeLocation.Instance);
            await _sut.AddAsync(eventToAdd);
            return eventToAdd;
        }
    }
}