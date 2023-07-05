using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommand : IRequest
{
    [Required(ErrorMessage = "Id attribute cannot be null")]
    public int AddressId {get; set;} // validação apenas

    [Required(ErrorMessage = "Id attribute cannot be null")]
    public int CustomerId {get; set;} // validação apenas
}

