using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Authors.Commands;
using Univali.Api.Features.Authors.Commands.CreateAuthor;
using Univali.Api.Features.Authors.Commands.UpdateAuthor;
using Univali.Api.Features.Authors.Queries;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/authors")]
public class AuthorController : MainController 
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator) 
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthorsWithCourses() 
    {
        var authorsToReturn = await _mediator.Send(
            new GetAllAuthorsWithCoursesQuery()
        );

        if (authorsToReturn == null) { return NoContent(); }

        return Ok(authorsToReturn);
    }

    [HttpGet("{authorId}", Name = "GetAuthorWithCourses")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthorDto>> GetAuthorWithCourses(
        int authorId
    ) {        
        var authorToReturn = await _mediator.Send(
            new GetAuthorWithCoursesQuery { AuthorId = authorId }
        );

        if (authorToReturn == null) return NotFound();

        return Ok(authorToReturn);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<AuthorDto>> CreateAuthorAsync(
        CreateAuthorCommand createAuthorCommand
    ) {
        var createAuthorCommandResponse = await _mediator.Send(createAuthorCommand);

        if (createAuthorCommandResponse.IsSuccessful == false) 
        {
            if (createAuthorCommandResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(createAuthorCommandResponse.Errors);
        }

        return CreatedAtRoute(
            "GetAuthorWithCourses",
            new { authorId = createAuthorCommandResponse.Author.AuthorId },
            createAuthorCommandResponse.Author
        );
    }

    [HttpPut("{authorId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAuthorAsync(
        int authorId, 
        UpdateAuthorCommand updateAuthorCommand
    ) {
        if(authorId != updateAuthorCommand.AuthorId) { return BadRequest(); }

        var updateAuthorCommandResponse = await _mediator.Send(updateAuthorCommand);

        if (updateAuthorCommandResponse.IsSuccessful == false) 
        {
            if (updateAuthorCommandResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(updateAuthorCommandResponse.Errors);
        }

        return NoContent();
    }

    [HttpDelete("{authorId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAuthorAsync(
        int authorId
    ) {
        var deletedAuthorEntity = await _mediator.Send( 
            new DeleteAuthorCommand { AuthorId = authorId } 
        );

        if(deletedAuthorEntity) { return NoContent(); }

        return NotFound();
    }
}
