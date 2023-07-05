using MediatR;
using Univali.Api.Features.Courses.Commands.CreateCourse;
using Univali.Api.Models;

namespace Univali.Api.Features.Courses.Commands;

public class CreateCourseCommand : IRequest<CreateCourseCommandResponse>
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public List<AuthorDto> Authors { get; set; } = new();

    public int PublisherId { get; set; }
}

