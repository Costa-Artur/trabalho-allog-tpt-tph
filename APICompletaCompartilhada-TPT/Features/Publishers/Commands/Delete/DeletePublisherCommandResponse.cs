// BRANCH: AULA
namespace Univali.Api.Features.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public DeletePublisherCommandResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}