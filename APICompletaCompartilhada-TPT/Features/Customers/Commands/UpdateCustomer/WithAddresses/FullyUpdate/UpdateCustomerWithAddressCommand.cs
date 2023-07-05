using MediatR;
using Univali.Api.Features.Addresses.Commands.UpdateCustomerWithAddress;
using Univali.Api.Models;

namespace Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddress;

public class UpdateCustomerWithAddressCommand : 
    IRequest<UpdateCustomerWithAddressCommandResponse>
{
    public int CustomerId { get; set; } 
    
    public string Name { get; set; } = string.Empty;

    public string Cpf { get; set; } = string.Empty;

    public ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();
}

