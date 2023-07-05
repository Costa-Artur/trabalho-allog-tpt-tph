using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Features.Courses.Commands.UpdateCourse;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Commands;

public class UpdateCourseCommandHandler :
    IRequestHandler<UpdateCourseCommand, UpdateCourseCommandResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IMapper _mapper;

    private readonly IValidator<UpdateCourseCommand> _validator;

    public UpdateCourseCommandHandler(
        IPublisherRepository publisherRepository,
        IMapper mapper, IValidator<UpdateCourseCommand> validator)
    {
        _publisherRepository = publisherRepository;

        _mapper = mapper;

        _validator = validator;
    }

    public async Task<UpdateCourseCommandResponse> Handle(
        UpdateCourseCommand request,
        CancellationToken cancellationToken)
    {
        UpdateCourseCommandResponse updateCourseCommandResponse = new();

        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.ToDictionary())
            {
                updateCourseCommandResponse.Errors.Add(error.Key, error.Value);
            }

            updateCourseCommandResponse.IsSuccessful = false;
            return updateCourseCommandResponse;
        }

        var courseEntity = await _publisherRepository.GetCourseByIdAsync(request.CourseId);

        _mapper.Map(request, courseEntity);

        await _publisherRepository.SaveChangesAsync();

        return updateCourseCommandResponse;
    }
}