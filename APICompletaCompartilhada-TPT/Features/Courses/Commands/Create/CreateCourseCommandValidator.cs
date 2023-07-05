using FluentValidation;
using Univali.Api.Features.Courses.Commands.CreateCourseWithAuthor;

namespace Univali.Api.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("You should fill out a Title")
            .MaximumLength(100)
            .WithMessage("The {PropertyName} shouldn't have more than 100 characteres");

        RuleFor(c => c.Description)//não é required no dbcontext
            .MaximumLength(100)
            .WithMessage("Description must be 100 characters or less");
        
        RuleFor(c => c.Price)
            .NotEmpty()
            .WithMessage("You should fill out a Price");


        RuleFor(c => c.Authors)
            .NotEmpty()
            .WithMessage("Course must have at least one Author")
            .ForEach(authorsRule =>
            {
                authorsRule.SetValidator(new CreateCourseWithAuthorValidator());
            })
            .When(c => c.Authors != null, ApplyConditionTo.CurrentValidator);

        RuleFor(a => a.PublisherId)
            .NotEmpty()
            .WithMessage("You should fill out a valid PublisherId");
    }
}