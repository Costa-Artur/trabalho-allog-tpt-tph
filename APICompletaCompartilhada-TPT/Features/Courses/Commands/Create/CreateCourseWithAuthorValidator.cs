using FluentValidation;
using Univali.Api.Entities;
using Univali.Api.Models;

namespace Univali.Api.Features.Courses.Commands.CreateCourseWithAuthor;

public class CreateCourseWithAuthorValidator : AbstractValidator<AuthorDto>
{
    public CreateCourseWithAuthorValidator()
    {
        RuleFor(a => a.FirstName)
            .Empty();

        RuleFor(a => a.LastName)
            .Empty();

        RuleFor(a => a.AuthorId)    
            .NotEmpty()
            .WithMessage("Fill a Id")      
            .GreaterThan(0)
            .WithMessage("The PublisherId must be greater than zero.");
    }
}