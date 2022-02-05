using FluentValidation;
using System;
namespace Bcan.Backend.Application.Features.Classes.Commands.Create
{
    // This validator only concerns about the application logic so
    // do not validate against the ShineClass invariants
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            // NotEmpty() is handled here bec AutoMapper ignores
            // the non-default ctor so null check is missing there.
            RuleFor(command => command.Title)
                .NotEmpty().MaximumLength(50);

            RuleFor(command => command.Start)
                .GreaterThan(DateTimeOffset.UtcNow);

            RuleFor(command => command.End)
                .GreaterThan(command => command.Start); 
        }
    }
}