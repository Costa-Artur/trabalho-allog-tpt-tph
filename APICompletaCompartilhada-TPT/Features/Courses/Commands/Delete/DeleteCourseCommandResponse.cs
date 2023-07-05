// BRANCH: AULA
namespace Univali.Api.Features.Courses.Commands;

public class DeleteCourseCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public DeleteCourseCommandResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}