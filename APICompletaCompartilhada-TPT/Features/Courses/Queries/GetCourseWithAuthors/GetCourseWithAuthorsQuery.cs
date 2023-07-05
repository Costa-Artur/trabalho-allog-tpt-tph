using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Courses.Queries;

public class GetCourseWithAuthorsQuery : IRequest<GetCourseWithAuthorsDto>
{
    [Required(ErrorMessage = "Error: PublisherId is required")]
    public int PublisherId { get; set; }

    [Required(ErrorMessage = "Error: CourseId is required")]
    public int CourseId { get; set; }
}