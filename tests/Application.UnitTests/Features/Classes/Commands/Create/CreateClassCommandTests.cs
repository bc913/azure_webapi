using AutoMapper;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Exceptions;
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
        private CreateClassCommand _command;

        public CreateClassCommandTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateClassProfile>();
            });

            _mapper = config.CreateMapper();
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            _command = new CreateClassCommand
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
        }

        [Fact]
        public async Task HandlerShouldSucceed()
        {
            // Given
            var expectedResult = Guid.Empty;

            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<Guid>>> successCall = (repo) => repo.AddAsync(It.IsAny<ShineClass>(), CancellationToken.None);
            mockRepo.Setup(successCall) //returns async call should match the parameter and return type successCall
                .ReturnsAsync((ShineClass toBeAdded, CancellationToken token) => { expectedResult = toBeAdded.Id; return toBeAdded.Id; })
                .Verifiable("AddAsync method should be called.");

            var handler = new CreateClassCommandHandler(mockRepo.Object, _mapper);
            // When
            var result = await handler.Handle(_command, CancellationToken.None);
            // Then
            mockRepo.Verify(successCall, Times.Once);
            result.Should().NotBe(Guid.Empty);
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void HandlerShouldFailIfTitleIsNull()
        {
            // Given
            _command.Title = null;

            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<Guid>>> successCall = (repo) => repo.AddAsync(It.IsAny<ShineClass>(), CancellationToken.None);
            mockRepo.Setup(successCall).ReturnsAsync(Guid.NewGuid()); // Do not care about the result since we expect this not to be called
    
            var handler = new CreateClassCommandHandler(mockRepo.Object, _mapper);

            // When
            Func<Task<Guid>> act = async () => await handler.Handle(_command, CancellationToken.None);

            // Then
            act.Should().ThrowAsync<ValidationException>();
            mockRepo.Verify(successCall, Times.Never);
        }

        [Fact]
        public void HandlerShouldFailIfTitleIsEmpty()
        {
            // Given
            _command.Title = string.Empty;
            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<Guid>>> successCall = (repo) => repo.AddAsync(It.IsAny<ShineClass>(), CancellationToken.None);
            mockRepo.Setup(successCall).ReturnsAsync(Guid.NewGuid()); // we don't care about the result since we expect this never get called.

            var handler = new CreateClassCommandHandler(mockRepo.Object, _mapper);

            // When
            Func<Task<Guid>> act = async () => await handler.Handle(_command, CancellationToken.None);

            // Then
            act.Should().ThrowAsync<ValidationException>();
            mockRepo.Verify(successCall, Times.Never);
        }

        [Fact]
        public void HandlerShouldFailIfTitleExceedsCharacterLimit()
        {
            // Given
            _command.Title = "This is a very very very very long title for a dance class so do not expect this happening in real world. It is imaginary.";
            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<Guid>>> successCall = (repo) => repo.AddAsync(It.IsAny<ShineClass>(), CancellationToken.None);
            mockRepo.Setup(successCall).ReturnsAsync(Guid.NewGuid()); // we don't care about the result since we expect this never get called.

            var handler = new CreateClassCommandHandler(mockRepo.Object, _mapper);

            // When
            Func<Task<Guid>> act = async () => await handler.Handle(_command, CancellationToken.None);

            // Then
            act.Should().ThrowAsync<ValidationException>();
            mockRepo.Verify(successCall, Times.Never);
        }

        [Fact]
        public void HandlerShouldFailIfStartDateIsPast()
        {
            // Given
            _command.Start = DateTimeOffset.UnixEpoch;

            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<Guid>>> successCall = (repo) => repo.AddAsync(It.IsAny<ShineClass>(), CancellationToken.None);
            mockRepo.Setup(successCall).ReturnsAsync(Guid.NewGuid()); // we don't care about the result since we expect this never get called.

            var handler = new CreateClassCommandHandler(mockRepo.Object, _mapper);

            // When
            Func<Task<Guid>> act = async () => await handler.Handle(_command, CancellationToken.None);

            // Then
            act.Should().ThrowAsync<ValidationException>();
            mockRepo.Verify(successCall, Times.Never);
        }

        [Fact]
        public void HandlerShouldFailIfEndDateIsSmallerThanStartDate()
        {
            // Given
            _command.Start = new DateTimeOffset(DateTime.UtcNow).AddDays(2);
            _command.End = new DateTimeOffset(DateTime.UtcNow).AddDays(1);

            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<Guid>>> successCall = (repo) => repo.AddAsync(It.IsAny<ShineClass>(), CancellationToken.None);
            mockRepo.Setup(successCall).ReturnsAsync(Guid.NewGuid()); // we don't care about the result since we expect this never get called.

            var handler = new CreateClassCommandHandler(mockRepo.Object, _mapper);

            // When
            Func<Task<Guid>> act = async () => await handler.Handle(_command, CancellationToken.None);

            // Then
            act.Should().ThrowAsync<ValidationException>();
            mockRepo.Verify(successCall, Times.Never);
        }


    }
}