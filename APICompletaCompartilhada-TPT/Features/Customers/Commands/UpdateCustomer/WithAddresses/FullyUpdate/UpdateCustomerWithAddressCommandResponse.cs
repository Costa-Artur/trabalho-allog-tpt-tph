using Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddress;

namespace Univali.Api.Features.Addresses.Commands.UpdateCustomerWithAddress;

public class UpdateCustomerWithAddressCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors { get; set; }

    public UpdateCustomerWithAddressDto CustomerWithAddress { get; set; } = default!;

    public UpdateCustomerWithAddressCommandResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}