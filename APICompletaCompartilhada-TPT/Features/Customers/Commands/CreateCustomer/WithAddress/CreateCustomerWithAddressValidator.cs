using FluentValidation;
using Univali.Api.Models;

namespace Univali.Api.Features.Customers.Commands.CreateCustomerWithAddress;

public class CreateCustomerWithAddressValidator : AbstractValidator<AddressDto>
{
    public CreateCustomerWithAddressValidator()
    {
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
    }
}