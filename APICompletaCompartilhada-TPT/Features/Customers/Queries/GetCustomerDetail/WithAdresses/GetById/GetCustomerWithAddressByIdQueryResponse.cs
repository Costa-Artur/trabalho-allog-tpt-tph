// BRANCH: AULA
namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerWithAddressByIdQueryResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public GetCustomerWithAddressesDto Customer {get; set;} = default!;

    public GetCustomerWithAddressByIdQueryResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}