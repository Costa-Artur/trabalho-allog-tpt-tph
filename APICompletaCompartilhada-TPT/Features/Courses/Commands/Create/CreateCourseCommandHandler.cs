using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Models;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler :
    IRequestHandler<CreateCourseCommand, CreateCourseCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<CreateCourseCommand> _validator;


    public CreateCourseCommandHandler(
        IPublisherRepository publisherRepository,
        IMapper mapper, IValidator<CreateCourseCommand> validator)
    {
        _publisherRepository = publisherRepository;

        _mapper = mapper;

        _validator = validator;

    }

public async Task<CreateCourseCommandResponse> Handle(
        CreateCourseCommand createCourseCommand, 
        CancellationToken cancellationToken
    ) {
        CreateCourseCommandResponse createCourseCommandResponse = new();

        var validationResult = _validator.Validate(createCourseCommand);

        if (validationResult.IsValid == false) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                createCourseCommandResponse.Errors
                    .Add(error.Key, error.Value);
            }

            createCourseCommandResponse.IsSuccessful = false;

            return createCourseCommandResponse;
        }

        var courseEntity = _mapper.Map<Course>(createCourseCommand);

        courseEntity.Authors.Clear();

        foreach (AuthorDto authorDto in createCourseCommand.Authors) 
        {
            var authorEntity = await _publisherRepository
                .GetAuthorByIdAsync(authorDto.AuthorId);

            if (authorEntity == null) { 
                createCourseCommandResponse.IsSuccessful = false;

                return createCourseCommandResponse;
            }

            courseEntity.Authors.Add(authorEntity);
        }

        var publisherFromDatabase = await _publisherRepository.GetPublisherByIdAsync(courseEntity.PublisherId);

        if (publisherFromDatabase == null)
        {
            createCourseCommandResponse.IsSuccessful = false;

            return createCourseCommandResponse;
        }

        _publisherRepository.AddCourse(courseEntity);

        await _publisherRepository.SaveChangesAsync();

        createCourseCommandResponse.Course = _mapper.Map<CreateCourseDto>(courseEntity);

        createCourseCommandResponse.Course.Publisher =_mapper.Map<PublisherWithoutCoursesDto>(publisherFromDatabase);

        return createCourseCommandResponse;
    }

}
