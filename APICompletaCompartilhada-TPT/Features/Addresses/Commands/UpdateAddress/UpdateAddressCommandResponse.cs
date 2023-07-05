namespace Univali.Api.Features.Addresses.Commands.UpdateAddresses;

public class UpdateAddressCommandResponse
{
    public bool IsSuccessful;
    public Dictionary<string, string[]> Errors {get;set;}
    
    public UpdateAddressCommandResponse ()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}