using FluentValidation;
using Bcan.Backend.Application.Dtos;

namespace Bcan.Backend.Application.Features.Classes.Commands.Create
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            RuleFor(command => command.Title)
                .NotEmpty().NotNull().MaximumLength(50);

            RuleFor(command => command.Info).DanceInfoMustBeValid();

        }
    }

    public static class ApplicationValidators
    {
        public static IRuleBuilderOptions<T, DanceInfoDto> DanceInfoMustBeValid<T>(this IRuleBuilder<T, DanceInfoDto> ruleBuilder) 
        {

            return ruleBuilder
                .Must(info => info.Types is not null).WithMessage("Dance info types should not be null")
                .Must(info => info.Types.Count > 0).WithMessage("Dance info types should be defined.");
        }
    }
}