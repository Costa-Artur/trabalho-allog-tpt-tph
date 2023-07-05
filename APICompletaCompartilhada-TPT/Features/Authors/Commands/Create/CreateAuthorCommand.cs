using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<CreateAuthorCommandResponse>
{
    public string FirstName {get; set;} = string.Empty;
    
    public string LastName {get; set;} = string.Empty;    
}


