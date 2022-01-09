using Bcan.Backend.Core.Entities;
using Bcan.Backend.Persistence.Contexts;
using Bcan.Backend.Persistence.Repositories;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestHelpers.Assert;
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
            var eventToAdd = new ShineEvent(Guid.NewGuid(), "Some title", ShineEventType.Social);
            await _sut.AddAsync(eventToAdd);
            return eventToAdd;
        }
    }
}