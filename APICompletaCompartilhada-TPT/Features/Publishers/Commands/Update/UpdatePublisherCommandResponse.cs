// BRANCH: AULA
namespace Univali.Api.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public UpdatePublisherCommandResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}