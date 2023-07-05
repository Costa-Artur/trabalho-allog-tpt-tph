namespace Univali.Api.Features.Customers.Queries.GetCustomerDetail;

public class GetCustomersDetailQueryResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public List<object> Customers {get; set;} = new();

    public GetCustomersDetailQueryResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}