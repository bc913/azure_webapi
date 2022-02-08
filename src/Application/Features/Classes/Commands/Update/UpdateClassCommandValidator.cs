using FluentValidation;
using System;

namespace Bcan.Backend.Application.Features.Classes.Commands.Update
{
    public class UpdateClassCommandValidator : AbstractValidator<UpdateClassCommand>
    {
        public UpdateClassCommandValidator()
        {
            RuleFor(command => command.Title)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(command => command.Start)
                .GreaterThan(DateTimeOffset.UtcNow);

            RuleFor(command => command.End)
                .GreaterThan(command => command.Start); 
        }
    }
}