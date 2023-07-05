using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Customers.Commands.DeleteCustomer;

// IRequest<> transforma a classe em uma Mensagem
// CreateCustomerDto é o tipo que este comando espera receber de volta
public class DeleteCustomerCommand : IRequest
{
    [Required(ErrorMessage = "Id attribute cannot be null")]
    public int CustomerId {get; set;} // validação apenas
}

