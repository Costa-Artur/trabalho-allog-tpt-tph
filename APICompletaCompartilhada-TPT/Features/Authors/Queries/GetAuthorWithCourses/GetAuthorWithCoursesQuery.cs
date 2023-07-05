using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Authors.Queries;

public class GetAuthorWithCoursesQuery : IRequest<GetAuthorWithCoursesDto?>
{
    [Required(ErrorMessage = "Error: AuthorId is required")]
    public int AuthorId { get; set; }
}