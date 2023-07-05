// TPT
namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public Object Customer {get; set;} = default!;

    public CreateCustomerCommandResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}