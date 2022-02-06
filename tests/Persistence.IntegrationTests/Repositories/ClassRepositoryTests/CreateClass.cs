using Bcan.Backend.Core.Entities;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Bcan.Backend.TestHelpers.Assert;
using Bcan.Backend.TestHelpers.FakeGenerators;
using Xunit;

namespace Bcan.Backend.Persistence.IntegrationTests.Repositories.ClassRepositoryTests
{
    public class CreateClass : RepositoryTestFixture
    {
        [Fact]
        public async Task AddAsyncShouldSucceed()
        {
            // Given
            var theClass = FakeShineClass.Instance;
            var sut = GetRepository<ShineClass>();

            // When
            var result = await sut.AddAsync(theClass);

            // Then
            result.Should().Be(theClass.Id);
            var query = await sut.GetByIdAsync(result, CancellationToken.None);
            query.Should().BeValueEqual(theClass);
        }

        [Fact]
        public async Task AddAsyncShouldFailIfAnEntityWithSameIdIsAdded()
        {
            // Given
            var theClass = FakeShineClass.Instance;
            var sut = GetRepository<ShineClass>();
            var result = await sut.AddAsync(theClass);

            // When
            var duplicateIdInstance = new ShineClass(theClass.Id,
                "some title", theClass.Info, theClass.Location, theClass.Time, theClass.Policy,
                theClass.Fee);
            
            Func<Task<Guid>> act = () => sut.AddAsync(duplicateIdInstance, CancellationToken.None);
            // Then
            await act.Should().ThrowAsync<Exception>();
        }
    }
}