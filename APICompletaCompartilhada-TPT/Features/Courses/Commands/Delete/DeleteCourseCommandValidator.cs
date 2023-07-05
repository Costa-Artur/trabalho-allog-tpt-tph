// BRANCH: AULA
using FluentValidation;

namespace Univali.Api.Features.Courses.Commands;

public class DeleteCourseCommandValidator :
    AbstractValidator<DeleteCourseCommand>
 {
    public DeleteCourseCommandValidator() 
    {
        RuleFor(c => c.CourseId)
            .NotEmpty()
            .WithMessage("Fill a Id")      
            .GreaterThan(0)
            .WithMessage("The CourseId must be greater than zero.");
    }
 }