using FluentValidation;

namespace Univali.Api.Features.Addresses.Commands.UpdateAddresses;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(a => a.AddressId)
            .NotEmpty()
            .WithMessage("The {PropertyName} cannot be null or empty");

        RuleFor(a => a.Street)
            .NotEmpty()
            .WithMessage("You should fill out a Street")
            .MaximumLength(50)
            .WithMessage("The {PropertyName} should'nt have more than 50 characters");

        RuleFor(a => a.City)
            .NotEmpty()
            .WithMessage("You should fill out a City")
            .MaximumLength(50)
            .WithMessage("The {PropertyName} should'nt have more than 50 characters");

        RuleFor(a => a.CustomerId)
            .NotEmpty()
            .WithMessage("The {PropertyName} cannot be null or empty");
        
    }
}