// TPT
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerByIdQuery : IRequest<GetCustomerDetailQueryResponse>
{
    [Required(ErrorMessage = "Id attribute cannot be null")]
    public int CustomerId {get; set;}
}