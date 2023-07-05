using FluentValidation;

namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(a => a.FirstName)
            .NotEmpty()
            .WithMessage("You should fill out a First Name")
            .MaximumLength(100)
            .WithMessage("The {PropertyName} shouldn't have more than 100 characters");

        RuleFor(a => a.LastName)
            .NotEmpty()
            .WithMessage("You should fill out a Last Name")
            .MaximumLength(100)
            .WithMessage("The {PropertyName} shouldn't have more than 100 characters");
    }
}