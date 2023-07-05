using MediatR;

namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomersWithAddressesQuery 
    : IRequest<List<GetCustomerWithAddressesDto>>
{
}