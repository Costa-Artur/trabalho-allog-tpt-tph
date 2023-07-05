using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Addresses.Queries.GetCustomerDetail;

public class GetAddressByIdDetailQuery : IRequest<GetAddressDetailDto>
{
    [Required(ErrorMessage = "Id attribute cannot be null")]
    public int CustomerId {get; set;}

    [Required(ErrorMessage = "Id attribute cannot be null")]
    public int AddressId {get; set;}
}