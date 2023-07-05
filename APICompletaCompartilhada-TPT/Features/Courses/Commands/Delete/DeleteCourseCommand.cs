using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.Features.Publishers.Commands.DeletePublisher;

namespace Univali.Api.Features.Courses.Commands;

public class DeleteCourseCommand : IRequest<DeleteCourseCommandResponse>
{
    public int CourseId { get; set; }
}

