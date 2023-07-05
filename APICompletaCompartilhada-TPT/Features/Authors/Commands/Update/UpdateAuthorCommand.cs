using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommand : IRequest<UpdateAuthorCommandResponse>
{
    public int AuthorId {get; set;}

   	public string FirstName {get; set;} = string.Empty;
	
   	public string LastName {get; set;} = string.Empty;
}


