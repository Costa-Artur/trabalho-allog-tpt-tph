using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommand : IRequest<CreateAddressCommandResponse>
{
    public int AddressId {get; set;}


    public string Street {get; set;} = string.Empty;


    public string City {get; set;} = string.Empty;


    public int CustomerId {get; set;}
}

