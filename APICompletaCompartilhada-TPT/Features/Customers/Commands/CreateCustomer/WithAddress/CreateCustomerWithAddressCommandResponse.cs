namespace Univali.Api.Features.Customers.Commands.CreateCustomerWithAddress;

public class CreateCustomerWithAddressCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get;set;}

    public CreateCustomerWithAddressDto CustomerWithAddress {get;set;} = default!;

    public CreateCustomerWithAddressCommandResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}