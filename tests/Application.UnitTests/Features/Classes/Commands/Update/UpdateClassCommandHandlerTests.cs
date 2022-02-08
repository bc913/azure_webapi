using AutoMapper;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Application.Dtos;
using Bcan.Backend.Application.Exceptions;
using Bcan.Backend.Application.Features.Classes.Commands.Update;
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

namespace Bcan.Backend.Application.UnitTests.Features.Classes.Commands.Update
{
    public class UpdateClassCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private UpdateClassCommand _command;

        public UpdateClassCommandHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UpdateClassProfile>();
            });
            _mapper = config.CreateMapper();
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            var info = new DanceInfoDto
            {
                Level = DanceLevelDto.Advanced,
                Types = new List<DanceTypeDto> { DanceTypeDto.Salsa, DanceTypeDto.Bachata }
            };
            var policy = new EventPolicyDto { DressCode = true, DanceShoes = true, Partner = false };

            IReadOnlyCollection<FeeOptionDto> options = new List<FeeOptionDto> 
            {
                new FeeOptionDto { Value = 90.0m, Individual = IndividualTypeDto.Student, Payment = PaymentTypeDto.OneTime, Description = "student desc" }, 
                new FeeOptionDto { Value = 180.0m, Individual = IndividualTypeDto.Regular, Payment = PaymentTypeDto.Financed, Description = "regular desc" }
            };
            var fee = new FeeDto { Options = options, Description = "some fee dto desc" };

            _command = new UpdateClassCommand
            {
                Id = FakeShineClass.Instance.Id,
                Title = "New Latin dances class",
                Info = info,
                Location = FakeLocationDto.Instance,
                Start = new DateTimeOffset(DateTime.UtcNow).AddDays(12),
                End = new DateTimeOffset(DateTime.UtcNow).AddDays(13),
                Fee = fee,
                Policy = policy,
                Description = "Some class description here"
            };
        }

        [Fact]
        public async Task HandlerShouldSucceed()
        {
            // Given
            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<ShineClass>>> successGetCall = (repo) => 
                repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None);
            
            mockRepo.Setup(successGetCall) //returns async call should match the parameter and return type successCall
                .ReturnsAsync((Guid id, CancellationToken token) => { return FakeShineClass.Instance; })
                .Verifiable("GetByIdAsync method should be called.");

            var sut = new UpdateClassCommandHandler(mockRepo.Object, _mapper);
            // When
            await sut.Handle(_command, default);
            // Then
            mockRepo.Verify(successGetCall, Times.Once);
            mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<ShineClass>(), default), Times.Once);
        }

        [Fact]
        public async Task HandlerShouldThrowWhenGivenIdDoesNotExist()
        {
            // Given
            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<ShineClass>>> getCall = (repo) => 
                repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None);
            
            mockRepo.Setup(getCall) //returns async call should match the parameter and return type successCall
                .ReturnsAsync((Guid id, CancellationToken token) => { return (ShineClass)null; })
                .Verifiable("GetByIdAsync method should be called.");

            var sut = new UpdateClassCommandHandler(mockRepo.Object, _mapper);
            // When
            Func<Task> act = () => sut.Handle(_command, default);
            // Then
            await act.Should().ThrowExactlyAsync<NotFoundException>();
            mockRepo.Verify(getCall, Times.Once);
            mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<ShineClass>(), default), Times.Never);
        }

        [Fact]
        public async Task HandlerShouldThrowWhenEmptyTitleIsGiven()
        {
            // Given
            _command.Title = string.Empty;

            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<ShineClass>>> getCall = (repo) => 
                repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None);
            
            mockRepo.Setup(getCall) //returns async call should match the parameter and return type successCall
                .ReturnsAsync((Guid id, CancellationToken token) => { return FakeShineClass.Instance; })
                .Verifiable("GetByIdAsync method should be called.");

            var sut = new UpdateClassCommandHandler(mockRepo.Object, _mapper);
            // When
            Func<Task> act = () => sut.Handle(_command, default);
            // Then
            await act.Should().ThrowExactlyAsync<ValidationException>();
            mockRepo.Verify(getCall, Times.Once);
            mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<ShineClass>(), default), Times.Never);
        }

        [Fact]
        public async Task HandlerShouldFailIfTitleExceedsCharacterLimit()
        {
            // Given
            _command.Title = "This is a very very very very long title for a dance class so do not expect this happening in real world. It is imaginary.";

            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<ShineClass>>> getCall = (repo) => 
                repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None);
            
            mockRepo.Setup(getCall) //returns async call should match the parameter and return type successCall
                .ReturnsAsync((Guid id, CancellationToken token) => { return FakeShineClass.Instance; })
                .Verifiable("GetByIdAsync method should be called.");

            var sut = new UpdateClassCommandHandler(mockRepo.Object, _mapper);
            // When
            Func<Task> act = () => sut.Handle(_command, default);
            // Then
            await act.Should().ThrowExactlyAsync<ValidationException>();
            mockRepo.Verify(getCall, Times.Once);
            mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<ShineClass>(), default), Times.Never);
        }

        [Fact]
        public async Task HandlerShouldFailIfStartDateIsPast()
        {
            // Given
            _command.Start = DateTimeOffset.UnixEpoch;

            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<ShineClass>>> getCall = (repo) => 
                repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None);
            
            mockRepo.Setup(getCall) //returns async call should match the parameter and return type successCall
                .ReturnsAsync((Guid id, CancellationToken token) => { return FakeShineClass.Instance; })
                .Verifiable("GetByIdAsync method should be called.");

            var sut = new UpdateClassCommandHandler(mockRepo.Object, _mapper);
            // When
            Func<Task> act = () => sut.Handle(_command, default);
            // Then
            await act.Should().ThrowExactlyAsync<ValidationException>();
            mockRepo.Verify(getCall, Times.Once);
            mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<ShineClass>(), default), Times.Never);
        }

        [Fact]
        public async Task HandlerShouldFailIfEndDateIsSmallerThanStartDate()
        {
            // Given
            _command.Start = new DateTimeOffset(DateTime.UtcNow).AddDays(2);
            _command.End = new DateTimeOffset(DateTime.UtcNow).AddDays(1);

            var mockRepo = new Mock<IRepository<ShineClass>>();
            Expression<Func<IRepository<ShineClass>, Task<ShineClass>>> getCall = (repo) => 
                repo.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None);
            
            mockRepo.Setup(getCall) //returns async call should match the parameter and return type successCall
                .ReturnsAsync((Guid id, CancellationToken token) => { return FakeShineClass.Instance; })
                .Verifiable("GetByIdAsync method should be called.");

            var sut = new UpdateClassCommandHandler(mockRepo.Object, _mapper);
            // When
            Func<Task> act = () => sut.Handle(_command, default);
            // Then
            await act.Should().ThrowExactlyAsync<ValidationException>();
            mockRepo.Verify(getCall, Times.Once);
            mockRepo.Verify(repo => repo.UpdateAsync(It.IsAny<ShineClass>(), default), Times.Never);
        }
    }
}