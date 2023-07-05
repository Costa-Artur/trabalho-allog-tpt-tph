using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Authors.Commands;

public class DeleteAuthorCommand : IRequest<bool>
{
    [Required(ErrorMessage = "Error: AuthorId is required")]
    public int AuthorId { get; set; }
}

