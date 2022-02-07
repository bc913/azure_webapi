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
    public class ClassDetailQuery : RepositoryTestFixture
    {
        [Fact]
        public async Task QueryClassDetailShouldSucceedForExistingRecord()
        {
            // Given
            var theClass = FakeShineClass.Instance;
            var sut = GetRepository<ShineClass>();
            var id = await sut.AddAsync(theClass);
            // When
            var actual = await sut.GetByIdAsync(id, CancellationToken.None);
            // Then
            actual.Should().BeValueEqual(theClass);
        }

        [Fact]
        public async Task QueryClassDetailShouldReturnNullForNonExistingRecord()
        {
            // Given
            var sut = GetRepository<ShineClass>();
            // When
            var actual = await sut.GetByIdAsync(Guid.NewGuid());
            // Then
            actual.Should().BeNull();
        }
    }
}