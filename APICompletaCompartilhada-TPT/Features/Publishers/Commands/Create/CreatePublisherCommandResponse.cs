// BRANCH: AULA
namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public CreatePublisherDto Publisher {get; set;} = default!;

    public CreatePublisherCommandResponse()
    {
        IsSuccessful = true;
        
        Errors = new Dictionary<string, string[]>();
    }
}