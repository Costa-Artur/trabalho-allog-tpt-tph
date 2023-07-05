namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public interface IGetCustomerQueryHandle 
{
    Task<GetCustomerDto?> Handle(GetCustomerQuery getCustomerQuery);
}