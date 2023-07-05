namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public CreateAuthorDto Author { get; set;} = default!;

    public CreateAuthorCommandResponse ()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}