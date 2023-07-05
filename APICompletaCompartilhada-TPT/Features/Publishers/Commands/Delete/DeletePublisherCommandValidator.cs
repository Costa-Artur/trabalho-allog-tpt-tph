// BRANCH: AULA
using FluentValidation;

namespace Univali.Api.Features.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommandValidator :
    AbstractValidator<DeletePublisherCommand>
 {
    public DeletePublisherCommandValidator() 
    {
        RuleFor(c => c.PublisherId)
        .NotEmpty()
        .WithMessage("Fill a Id")      
        .GreaterThan(0)
        .WithMessage("The PublisherId must be greater than zero.");
    }
 }