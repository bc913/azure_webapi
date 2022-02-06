using Bcan.Backend.Core.Entities;
using Bcan.Backend.Persistence.Repositories;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bcan.Backend.TestHelpers.Assert;
using Bcan.Backend.TestHelpers.FakeGenerators;
using Xunit;

namespace Bcan.Backend.Persistence.IntegrationTests.Repositories.ClassRepositoryTests
{
    public class CreateClass : RepositoryTestFixture
    {
        private readonly Repository<ShineClass> _sut;

        public CreateClass()
        {
            _sut = GetRepository<ShineClass>();
        }

        [Fact]
        public async Task AddAsyncShouldSucceed()
        {
            // Given
            var theClass = FakeShineClass.Instance;

            // When
            var result = await _sut.AddAsync(theClass);

            // Then
            result.Should().Be(theClass.Id);
            var query = await _sut.GetByIdAsync(result, CancellationToken.None);
            query.Should().BeValueEqual(theClass);
        }
    }
}