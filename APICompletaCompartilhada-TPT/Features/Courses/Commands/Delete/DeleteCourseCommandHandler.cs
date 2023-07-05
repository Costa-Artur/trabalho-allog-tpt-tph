using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Commands;

public class DeleteCourseCommandHandler : 
    IRequestHandler<DeleteCourseCommand, DeleteCourseCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    public readonly IValidator<DeleteCourseCommand> _validator;

    private readonly IMapper _mapper;

    public DeleteCourseCommandHandler(
        IPublisherRepository publisherRepository, 
        IValidator<DeleteCourseCommand> validator,
        IMapper mapper

    ) {
        _publisherRepository = publisherRepository;

        _validator = validator;

        _mapper = mapper;
    }

    public async Task<DeleteCourseCommandResponse> Handle(
        DeleteCourseCommand deleteCourseCommand, 
        CancellationToken cancellationToken
    ) {

        DeleteCourseCommandResponse deleteCourseCommandResponse = new ();

        var validationResult = _validator.Validate(deleteCourseCommand);

        if (validationResult.IsValid == false) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                deleteCourseCommandResponse.Errors
                    .Add(error.Key, error.Value);
            }

            deleteCourseCommandResponse.IsSuccessful = false;

            return deleteCourseCommandResponse;
        } 

        var courseFromDatabase = await _publisherRepository
            .GetCourseByIdAsync(deleteCourseCommand.CourseId);

        if (courseFromDatabase == null) 
        { 
            deleteCourseCommandResponse.IsSuccessful = false; 

            return deleteCourseCommandResponse;
        }

        _publisherRepository.RemoveCourse(courseFromDatabase);

        await _publisherRepository.SaveChangesAsync();

        return deleteCourseCommandResponse;

    }
}
