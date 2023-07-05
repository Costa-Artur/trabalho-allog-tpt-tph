using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.Models;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Addresses.Commands.UpdateAddresses;

public class UpdateAddressCommand : IRequest<UpdateAddressCommandResponse>
{
    public int AddressId {get; set;}

    public string Street {get; set;} = string.Empty;

    public string City {get; set;} = string.Empty;

    public int CustomerId {get; set;}
}

