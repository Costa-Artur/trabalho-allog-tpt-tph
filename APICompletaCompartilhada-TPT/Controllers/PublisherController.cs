using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Publishers.Commands;
using Univali.Api.Features.Publishers.Commands.CreatePublisher;
using Univali.Api.Features.Publishers.Commands.DeletePublisher;
using Univali.Api.Features.Publishers.Commands.UpdatePublisher;
using Univali.Api.Features.Publishers.Queries;
using Univali.Api.Features.Publishers.Queries.GetPublisher;
using Univali.Api.Models;

namespace Univali.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/publishers")]
public class PublisherController : MainController 
{
    private readonly IMediator _mediator;

    public PublisherController(IMediator mediator) 
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PublisherDto>>> GetAllPublishersWithCourses() 
    {
        var getAllPublishersWithCoursesQueryResponse = await _mediator.Send(
            new GetAllPublishersWithCoursesQuery()
        );

        if (getAllPublishersWithCoursesQueryResponse.IsSuccessful == false) 
        {
            if (getAllPublishersWithCoursesQueryResponse.Errors.Count == 0) 
            {
                return NotFound(); 
            }

            return Validate(getAllPublishersWithCoursesQueryResponse.Errors);
        }

        return Ok(getAllPublishersWithCoursesQueryResponse.Publisher);
    }

    [HttpGet("{publisherId}", Name = "GetPublisherWithCourses")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PublisherDto>> GetPublisherWithCourses(
        int publisherId
    ) {
        var GetPublisherWithCoursesQueryResponse = await _mediator.Send(
            new GetPublisherWithCoursesQuery{PublisherId = publisherId}
        );

        if (GetPublisherWithCoursesQueryResponse.IsSuccessful == false) 
        {
            if (GetPublisherWithCoursesQueryResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(GetPublisherWithCoursesQueryResponse.Errors);
        }

        return Ok(GetPublisherWithCoursesQueryResponse.Publisher);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<CreatePublisherDto?>> CreateAsync(
        CreatePublisherCommand createPublisherCommand
    ) {        
        var createPublisherCommandResponse = await _mediator.Send(createPublisherCommand);

        if (createPublisherCommandResponse.IsSuccessful == false) 
        {
            return Validate(createPublisherCommandResponse.Errors);
        }

        if (createPublisherCommandResponse.Publisher == null) 
        { 
            return BadRequest("O publsiher informado não existe"); 
        }

        return CreatedAtRoute (
            "GetPublisherWithCourses",
            new { publisherId = createPublisherCommandResponse.Publisher.PublisherId },
            createPublisherCommandResponse.Publisher
        );
    }

    [HttpPut("{publisherId}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateAsync(
        int publisherId, 
        UpdatePublisherCommand updatePublisherCommand
    ) {
        if(publisherId != updatePublisherCommand.PublisherId) { return BadRequest(); }

        var updatedPublisherCommandResponse = await _mediator.Send(updatePublisherCommand);

        if (updatedPublisherCommandResponse.IsSuccessful == false) 
        {
            if (updatedPublisherCommandResponse.Errors.Count() == 0) 
            {
                return NotFound(); 
            }

            return Validate(updatedPublisherCommandResponse.Errors);
        }

         return NoContent();
    }

    [HttpDelete("{publisherId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAsync(
        int publisherId
    ) {
        var deletedPublisherCommandResponse = await _mediator.Send(
            new DeletePublisherCommand{PublisherId = publisherId} 
        );

        if (deletedPublisherCommandResponse.IsSuccessful == false) 
        {
            if (deletedPublisherCommandResponse.Errors.Count() == 0) 
            {
                return NotFound();
            }

            return Validate(deletedPublisherCommandResponse.Errors);
        }

        return NoContent();
    }
}
