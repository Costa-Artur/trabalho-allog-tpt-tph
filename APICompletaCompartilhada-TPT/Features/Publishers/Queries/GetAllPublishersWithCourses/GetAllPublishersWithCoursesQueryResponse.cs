// BRANCH: AULA
namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

public class GetAllPublishersWithCoursesQueryResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public List<GetPublisherWithCoursesDto> Publisher {get; set;} = new();

    public GetAllPublishersWithCoursesQueryResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}