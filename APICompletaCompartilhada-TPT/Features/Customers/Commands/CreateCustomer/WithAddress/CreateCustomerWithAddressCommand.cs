using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Models;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.CreateCustomerWithAddress;

public class CreateCustomerWithAddressCommand : IRequest<CreateCustomerWithAddressCommandResponse>
{
    public string Name {get; set;} = string.Empty;

    public string Cpf {get; set;} = string.Empty;
    
    public ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();
}

