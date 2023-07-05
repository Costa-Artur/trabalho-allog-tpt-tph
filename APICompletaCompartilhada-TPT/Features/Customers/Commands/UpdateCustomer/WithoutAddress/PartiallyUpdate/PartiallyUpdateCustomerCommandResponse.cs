namespace Univali.Api.Features.Customers.Commands.PartiallyUpdateCustomer;

public class PartiallyUpdateCustomerCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public PartiallyUpdateCustomerCommandResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}