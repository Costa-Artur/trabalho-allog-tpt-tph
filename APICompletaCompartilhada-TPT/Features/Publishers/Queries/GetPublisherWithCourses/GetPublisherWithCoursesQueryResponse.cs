// BRANCH: AULA
namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

public class GetPublisherWithCoursesQueryResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public GetPublisherWithCoursesDto Publisher {get; set;} = default!;

    public GetPublisherWithCoursesQueryResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}