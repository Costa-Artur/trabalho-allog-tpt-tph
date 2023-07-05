using Univali.Api.Models;

namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

public class GetPublisherWithCoursesDto
{
    public int PublisherId { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
    public List<CourseWithoutAuthorDto> Courses { get; set; } = new ();
}
