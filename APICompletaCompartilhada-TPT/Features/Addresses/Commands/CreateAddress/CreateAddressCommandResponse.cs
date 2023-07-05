namespace Univali.Api.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public CreateAddressDto Address { get; set;} = default!;

    public CreateAddressCommandResponse ()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}