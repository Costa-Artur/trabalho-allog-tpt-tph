using Univali.Api.Models;

namespace Univali.Api.Features.Authors.Queries
{
    public class GetAuthorWithCoursesDto
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public List<CourseWithoutAuthorDto> Courses { get; set; } = new ();
    }
}
