using MediatR;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerWithAddressByIdQuery : 
    IRequest<GetCustomerWithAddressByIdQueryResponse>
{
    public int CustomerId {get; set;}
}