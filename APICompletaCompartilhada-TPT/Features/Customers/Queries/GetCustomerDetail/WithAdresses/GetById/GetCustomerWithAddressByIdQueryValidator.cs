// BRANCH: AULA
using FluentValidation;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerWithAddressByIdQueryValidator :
    AbstractValidator<GetCustomerWithAddressByIdQuery>
 {
    public GetCustomerWithAddressByIdQueryValidator() 
    {
        RuleFor(c => c.CustomerId)
        .NotEmpty()
        .WithMessage("Fill a Id")      
        .GreaterThan(0)
        .WithMessage("The CustomerId must be greater than zero.");
    }
 }