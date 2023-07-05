using FluentValidation;

namespace Univali.Api.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(a => a.AuthorId)
            .NotEmpty()
            .WithMessage("The {PropertyName} cannot be null or empty");

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