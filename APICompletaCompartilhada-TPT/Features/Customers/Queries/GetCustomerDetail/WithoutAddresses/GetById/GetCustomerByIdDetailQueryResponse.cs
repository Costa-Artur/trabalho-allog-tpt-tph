namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomerDetailQueryResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public GetCustomerDto Customer {get; set;} = new();

    public GetCustomerDetailQueryResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}