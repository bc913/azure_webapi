using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using AutoMapper;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Application.Features.Events.Queries.GetEvents;
using Bcan.Backend.Application.Contracts.Repositories;

namespace Bcan.Backend.Application.UnitTests.Features.Events.Queries.GetUsers
{
    public class GetEventsQueryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReadRepository<ShineEvent>> _mockRepo;

        public GetEventsQueryTests()
        {
            _mockRepo = MockEventRepository.Instance();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShineEventLiteProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetEventsShouldSucceed()
        {
            //Given
            var handler = new GetEventsQueryHandler(_mapper, _mockRepo.Object);
            //When
            var result = await handler.Handle(new GetEventsQuery(), CancellationToken.None);
            //Then
            result.Should().BeOfType<List<ShineEventLiteDto>>();
            result.Should().HaveCount(4);
        }
    }
}