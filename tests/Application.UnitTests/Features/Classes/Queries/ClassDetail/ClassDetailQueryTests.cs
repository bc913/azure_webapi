using AutoMapper;
using Bcan.Backend.Application.Exceptions;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Application.Features.Classes.Queries.ShineClassDetail;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.TestHelpers.FakeGenerators;
using FluentAssertions;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Features.Classes.Queries.ShineClassDetail
{
    public class ShineClassDetailQueryTests
    {
        private readonly IMapper _mapper;
        public ShineClassDetailQueryTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShineClassDetailProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task HandlerShouldSucceed()
        {
            // Given
            var entity = FakeShineClass.Instance;
            var mockRepo = new Mock<IReadRepository<ShineClass>>();

            Expression<Func<IReadRepository<ShineClass>, Task<ShineClass>>> successCall = 
                (repo) => repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None);
            mockRepo.Setup(successCall).ReturnsAsync(entity).Verifiable("GetByIdAsync method should be called.");

            var sut = new ShineClassDetailQueryHandler(mockRepo.Object, _mapper);
            var request = new ShineClassDetailQuery { Id = entity.Id };

            // When
            var result = await sut.Handle(request, CancellationToken.None);

            // Then
            mockRepo.Verify(successCall, Times.AtLeastOnce);
            result.Should().BeEquivalentTo<ShineClass>(entity, 
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
            
            result.Start.Should().Be(entity.Time.Start);
            result.End.Should().Be(entity.Time.End);
        }

        [Fact]
        public async Task HandlerShouldFailIfNoEntityFoundForGivenId()
        {
            // Given
            var mockRepo = new Mock<IReadRepository<ShineClass>>();
            Expression<Func<IReadRepository<ShineClass>, Task<ShineClass>>> notFoundCall = 
                (repo) => repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None);
            mockRepo
                .Setup(notFoundCall)
                .ReturnsAsync((ShineClass)null)
                .Verifiable("GetByIdAsync method should be called and return null");

            var request = new ShineClassDetailQuery { Id = Guid.NewGuid() };
            var sut = new ShineClassDetailQueryHandler(mockRepo.Object, _mapper);
            // When
            Func<Task<ShineClassDetailDto>> act = () => sut.Handle(request, CancellationToken.None);
            // Then
            await act.Should().ThrowExactlyAsync<NotFoundException>();
            mockRepo.Verify(notFoundCall, Times.AtLeastOnce);
        }
    }
}
