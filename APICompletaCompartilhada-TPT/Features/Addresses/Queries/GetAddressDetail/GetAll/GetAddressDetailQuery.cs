using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Addresses.Queries.GetCustomerDetail;

public class GetAddressDetailQuery : IRequest<List<GetAddressDetailDto>>
{
    [Required(ErrorMessage = "Id attribute cannot be null")]
    public int CustomerId {get; set;}
}