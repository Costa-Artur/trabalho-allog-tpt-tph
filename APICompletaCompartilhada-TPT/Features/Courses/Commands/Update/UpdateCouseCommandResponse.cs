namespace Univali.Api.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandResponse
{
    public bool IsSuccessful;
    public Dictionary<string, string[]> Errors {get; set;}

    public UpdateCourseCommandResponse()
    {
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}