namespace Univali.Api.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get;set;}
    
    public UpdateAuthorCommandResponse ()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}