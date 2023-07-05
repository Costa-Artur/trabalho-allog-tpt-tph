using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Courses.Commands;
using Univali.Api.Features.Courses.Queries;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/publishers/{publisherId}/courses")]
public class CourseController : MainController 
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator) 
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCoursesWithAuthors(
        int publisherId
    ) {
        var coursesToReturn = await _mediator.Send(
            new GetAllCoursesWithAuthorsQuery { PublisherId = publisherId }
        );

        if (coursesToReturn == null) { return NoContent(); }

        return Ok(coursesToReturn);
    }

    [HttpGet("{courseId}", Name = "GetCourseWithAuthors")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CourseDto>> GetCourseWithAuthors(
        int publisherId,
        int courseId
    ) {        
        var courseToReturn = await _mediator.Send(
            new GetCourseWithAuthorsQuery { PublisherId = publisherId, CourseId = courseId }
        );

        if (courseToReturn == null) { return NotFound(); }

        return Ok(courseToReturn);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreateCourseDto?>> CreateAsync(
        CreateCourseCommand createCourseCommand,
        int publisherId
    ) {        
        if (publisherId != createCourseCommand.PublisherId) 
        {
            return BadRequest("Erro: PublisherId difere da rota");
        }
        
        var createCourseCommandResponse = await _mediator.Send(createCourseCommand);

        if (createCourseCommandResponse == null) 
        { 
            return BadRequest("O Autor informado não existe"); 
        }

        if (createCourseCommandResponse.IsSuccessful == false) 
        {
            if (createCourseCommandResponse.Errors.Count() == 0) 
            {
                return NotFound();
            }

            return Validate(createCourseCommandResponse.Errors);
        }

        return CreatedAtRoute(
            "GetCourseWithAuthors",
            new { 
                courseId = createCourseCommandResponse.Course.CourseId , 
                publisherId = createCourseCommandResponse.Course.Publisher?.PublisherId 
            },
            createCourseCommandResponse.Course
        );
    }

    [HttpPut("{courseId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAsync(
        int publisherId,
        int courseId, 
        UpdateCourseCommand updateCourseCommand
    ) {
        // Ao invés desse IF, poderia validar para alteração do publisher a partir do ID
        if(courseId != updateCourseCommand.CourseId
            || publisherId != updateCourseCommand.PublisherId
        ) { 
            return BadRequest(); 
        }

        var updateCourseCommandToReturn = await _mediator.Send(updateCourseCommand);

        if (updateCourseCommandToReturn.IsSuccessful == false) 
        {
            if (updateCourseCommandToReturn.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(updateCourseCommandToReturn.Errors);
        }

        return NoContent();    
    }

    [HttpDelete("{courseId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync(
        int courseId
    ) {
        var deleteCourseCommandResponse = await _mediator.Send(
            new DeleteCourseCommand{CourseId = courseId} 
        );

        if (deleteCourseCommandResponse.IsSuccessful == false) 
        {
            if (deleteCourseCommandResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(deleteCourseCommandResponse.Errors);
        }

        return NoContent();
    }
}
