using Bcan.Backend.Core.Entities;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Bcan.Backend.TestHelpers.Assert;
using Bcan.Backend.TestHelpers.FakeGenerators;
using Xunit;

namespace Bcan.Backend.Persistence.IntegrationTests.Repositories.ClassRepositoryTests
{
    public class UpdateClass : RepositoryTestFixture
    {
        [Fact]
        public async Task UpdateAsyncShouldSucceedForExistingEntity()
        {
            // Given
            var entity = new ShineClass(FakeShineClass.Instance.Id, FakeShineClass.Instance.Title,
                FakeShineClass.Instance.Info, FakeShineClass.Instance.Location, 
                FakeShineClass.Instance.Time, FakeShineClass.Instance.Policy, FakeShineClass.Instance.Fee,
                FakeShineClass.Instance.Description, FakeShineClass.Instance.Media);

            var sut = GetRepository<ShineClass>();
            var result = await sut.AddAsync(entity);
            //var actual = await sut.GetByIdAsync(entity.Id, default);

            // When
            var expectedTitle = "This the updated title";
            entity.Title = expectedTitle;
            await sut.UpdateAsync(entity, default);

            // Then
            var actual = await sut.GetByIdAsync(entity.Id, default);
            actual.Should().BeValueEqual(entity);
        }

        [Fact]
        public async Task UpdateAsyncShouldThrowForNonExistingEntity()
        {
            // Given
            var sut = GetRepository<ShineClass>();
            var entity = new ShineClass(FakeShineClass.Instance.Id, FakeShineClass.Instance.Title,
                FakeShineClass.Instance.Info, FakeShineClass.Instance.Location, 
                FakeShineClass.Instance.Time, FakeShineClass.Instance.Policy, FakeShineClass.Instance.Fee,
                FakeShineClass.Instance.Description, FakeShineClass.Instance.Media);

            // When
            Func<Task> act = () => sut.UpdateAsync(entity, default);

            // Then
            await act.Should().ThrowAsync<Exception>();
        }
    }
}