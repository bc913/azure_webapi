using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Application.Exceptions;
using Bcan.Backend.Application.Features.Classes.Commands.Delete;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.TestHelpers.FakeGenerators;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Features.Classes.Commands.Delete
{
    public class DeleteClassCommandTests
    {
        [Fact]
        public async Task HandlerShouldSucceedForExistingEntity()
        {
            // Given
            var entity = FakeShineClass.Instance;
            
            var mockRepo = new Mock<IRepository<ShineClass>>();

            Expression<Func<IRepository<ShineClass>, Task<ShineClass>>> getCall = 
                (repo) => repo.GetByIdAsync(It.IsAny<Guid>(), default);
            mockRepo.Setup(getCall)
                    .ReturnsAsync(entity)
                    .Verifiable("GetByIdAsync should be called.");

            Expression<Func<IRepository<ShineClass>, Task>> deleteCall = 
                    (repo) => repo.DeleteAsync(It.IsAny<ShineClass>(), default);
            //mockRepo.Setup(deleteCall).ReturnsAsync(new Task(() => { }).Verifiable("DeleteAsync should be called");

            var request = new DeleteClassCommand { Id = entity.Id };
            var sut = new DeleteClassCommandHandler(mockRepo.Object);
            // When
            await sut.Handle(request, default);
            // Then
            mockRepo.Verify(getCall, Times.Once);
            mockRepo.Verify(deleteCall, Times.Once);
        }

        [Fact]
        public async Task HandlerShouldFailForNonExistingEntity()
        {
            // Given
            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<ShineClass>>> getCall = 
                (repo) => repo.GetByIdAsync(It.IsAny<Guid>(), default);
            mockRepo.Setup(getCall).ReturnsAsync((ShineClass)null).Verifiable("GetByIdAsync should be called at least once.");

            var request = new DeleteClassCommand { Id = Guid.NewGuid() };
            var sut = new DeleteClassCommandHandler(mockRepo.Object);
            // When
            Func<Task<Unit>> act = () => sut.Handle(request, default);
            // Then
            await act.Should().ThrowAsync<NotFoundException>();
            mockRepo.Verify(getCall, Times.Once);
            mockRepo.Verify(r => r.DeleteAsync(It.IsAny<ShineClass>(), default), Times.Never);
        }
    }
}