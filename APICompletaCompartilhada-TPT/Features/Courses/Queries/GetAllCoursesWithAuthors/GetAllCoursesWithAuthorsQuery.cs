using MediatR;

namespace Univali.Api.Features.Courses.Queries;

public class GetAllCoursesWithAuthorsQuery : IRequest<List<GetCourseWithAuthorsDto>?>
{
    public int PublisherId { get; set; }
}