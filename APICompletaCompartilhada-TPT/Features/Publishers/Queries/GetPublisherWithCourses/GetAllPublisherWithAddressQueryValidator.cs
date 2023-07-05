// BRANCH: AULA
using FluentValidation;

namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

public class GetPublisherWithCoursesQueryValidator :
    AbstractValidator<GetPublisherWithCoursesQuery>
 {
    public GetPublisherWithCoursesQueryValidator() 
    {
        RuleFor(c => c.PublisherId)
        .NotEmpty()
        .WithMessage("Fill a Id")      
        .GreaterThan(0)
        .WithMessage("The PublisherId must be greater than zero.");
    }
 }