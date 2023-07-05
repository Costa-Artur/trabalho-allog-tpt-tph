using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerPartiallyCommand : IRequest
{
    [Required(ErrorMessage = "Id attribute cannot be null")]
    public int CustomerId {get; set;} // validação apenas

    [MaxLength(100, ErrorMessage = "The name shouldn't have more than 100 characters")]
    public string Name {get; set;} = string.Empty;

    [CPFValidationAttribute(ErrorMessage = "CPF must be valid")]
    public string Cpf {get; set;} = string.Empty;
}

