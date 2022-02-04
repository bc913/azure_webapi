using AutoMapper;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Features.Classes.Commands.Create;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.TestHelpers.FakeGenerators;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Bcan.Backend.Application.UnitTests.Features.Classes.Commands.Create
{
    public class CreateClassCommandTests
    {
        private readonly IMapper _mapper;
        public CreateClassCommandTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateClassProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task CreateClassShouldSucceed()
        {
            // Given
            var mockRepo = new Mock<IRepository<ShineClass>>();
            mockRepo.Setup(repo => repo
                .AddAsync(It.IsAny<ShineClass>(), CancellationToken.None))
                .ReturnsAsync((ShineClass added, CancellationToken token) => { return added.Id; })
                .Verifiable("AddAsync method should be called.");

            var handler = new CreateClassCommandHandler(mockRepo.Object, _mapper);
            var command = new CreateClassCommand
            {
                Title = "New Latin dances class",
                Info = new DanceInfoDto
                {
                    Level = DanceLevelDto.Advanced,
                    Types = new List<DanceTypeDto> { DanceTypeDto.Salsa, DanceTypeDto.Bachata }
                },
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = new FeeDto 
                { 
                    Options = new List<FeeOptionDto> 
                    {
                        new FeeOptionDto { Value = 90.0m, Individual = IndividualTypeDto.Student, Payment = PaymentTypeDto.OneTime, Description = "student desc" }, 
                        new FeeOptionDto { Value = 180.0m, Individual = IndividualTypeDto.Regular, Payment = PaymentTypeDto.Financed, Description = "regular desc" }
                    }, 
                    Description = "some fee dto desc" 
                },
                Policy = new EventPolicyDto { DressCode = true, DanceShoes = true, Partner = false },
                Description = "Some class description here"
            };
            // When
            var result = await handler.Handle(command, CancellationToken.None);
            // Then
            mockRepo.Verify(repo => repo.AddAsync(It.IsAny<ShineClass>(), default), Times.Once);
        }

        [Fact]
        public async Task CreateClassShouldSucceed2()
        {
            // Given
            var expectedResult = Guid.Empty;
            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<Guid>>> successCall = (repo) => repo.AddAsync(It.IsAny<ShineClass>(), CancellationToken.None);
            mockRepo.Setup(successCall)
                .ReturnsAsync((ShineClass added, CancellationToken token) => { expectedResult = added.Id; return added.Id; })
                .Verifiable("AddAsync method should be called.");

            var handler = new CreateClassCommandHandler(mockRepo.Object, _mapper);
            var command = new CreateClassCommand
            {
                Title = "New Latin dances class",
                Info = new DanceInfoDto
                {
                    Level = DanceLevelDto.Advanced,
                    Types = new List<DanceTypeDto> { DanceTypeDto.Salsa, DanceTypeDto.Bachata }
                },
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = new FeeDto 
                { 
                    Options = new List<FeeOptionDto> 
                    {
                        new FeeOptionDto { Value = 90.0m, Individual = IndividualTypeDto.Student, Payment = PaymentTypeDto.OneTime, Description = "student desc" }, 
                        new FeeOptionDto { Value = 180.0m, Individual = IndividualTypeDto.Regular, Payment = PaymentTypeDto.Financed, Description = "regular desc" }
                    }, 
                    Description = "some fee dto desc" 
                },
                Policy = new EventPolicyDto { DressCode = true, DanceShoes = true, Partner = false },
                Description = "Some class description here"
            };
            // When
            var result = await handler.Handle(command, CancellationToken.None);
            // Then
            mockRepo.Verify(successCall, Times.Once);
            result.Should().NotBe(Guid.Empty);
            result.Should().Be(expectedResult);
        }
    }
}