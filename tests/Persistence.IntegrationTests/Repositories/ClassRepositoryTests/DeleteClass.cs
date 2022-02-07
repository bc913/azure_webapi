using Bcan.Backend.Core.Entities;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Bcan.Backend.TestHelpers.Assert;
using Bcan.Backend.TestHelpers.FakeGenerators;
using Xunit;

namespace Bcan.Backend.Persistence.IntegrationTests.Repositories.ClassRepositoryTests
{
    public class DeleteClass : RepositoryTestFixture
    {
        [Fact]
        public async Task DeletetingExistingClassShouldSucceed()
        {
            // Given
            var entity = FakeShineClass.Instance;
            var sut = GetRepository<ShineClass>();
            var id = await sut.AddAsync(entity, default);
            // When
            await sut.DeleteAsync(entity, default);
            // Then
            var actual = await sut.GetByIdAsync(entity.Id, default);
            actual.Should().BeNull();
        }

        [Fact]
        public void DeletingNonExistingClassShouldFail()
        {
            // Given
            var entity = FakeShineClass.Instance;
            var sut = GetRepository<ShineClass>();
            // When
            Func<Task> act = () => sut.DeleteAsync(entity, default);
            // Then
            act.Should().ThrowAsync<Exception>();
        }
    }
}