using FluentValidation;
using System;
namespace Bcan.Backend.Application.Features.Classes.Commands.Create
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            // NotEmpty() is handled here bec AutoMapper ignores
            // the non-default ctor so null check is missing there.
            RuleFor(command => command.Title)
                .NotEmpty().MaximumLength(50);

            RuleFor(command => command.Start)
                .NotEmpty()
                .NotNull()
                .GreaterThan(DateTimeOffset.UtcNow);
        }
    }
}