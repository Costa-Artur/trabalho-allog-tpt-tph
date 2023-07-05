using FluentValidation;

namespace Univali.Api.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(a => a.CourseId)
            .NotEmpty()
            .WithMessage("The {PropertyName} cannot be null or empty");

        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("You should fill out a Title")
            .MaximumLength(100)
            .WithMessage("The {PropertyName} shouldn't have more than 100 characteres");

        RuleFor(c => c.Description)
            .MaximumLength(100)
            .WithMessage("Description must be 100 characters or less");

        RuleFor(c => c.Price)
            .NotEmpty()
            .WithMessage("You should fill out a Price");

        RuleFor(a => a.PublisherId)
            .NotEmpty()
            .WithMessage("You should fill out a valid PublisherId");
    }
}