using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Features.Publishers.Queries;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

public class GetPublisherWithCoursesQueryHandler : 
    IRequestHandler<GetPublisherWithCoursesQuery, GetPublisherWithCoursesQueryResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IMapper _mapper;

    public readonly IValidator<GetPublisherWithCoursesQuery> _validator;

    public GetPublisherWithCoursesQueryHandler(
        IPublisherRepository publisherRepository, 
        IMapper mapper,
        IValidator<GetPublisherWithCoursesQuery> validator
    ) {
        _publisherRepository = publisherRepository;

        _mapper = mapper;

        _validator = validator;
    }

 public async Task<GetPublisherWithCoursesQueryResponse> Handle(
        GetPublisherWithCoursesQuery getPublisherWithCoursesQuery, 
        CancellationToken cancellationToken
    ) {
        GetPublisherWithCoursesQueryResponse getPublisherWithCoursesQueryResponse = new();

        var validationResult = _validator.Validate(getPublisherWithCoursesQuery);

        if (validationResult.IsValid == false) 
        {
            foreach (var error in validationResult.ToDictionary()) 
            {
                getPublisherWithCoursesQueryResponse.Errors
                    .Add(error.Key, error.Value);
            }

            getPublisherWithCoursesQueryResponse.IsSuccessful = false;

            return getPublisherWithCoursesQueryResponse;
        }

        var publisherFromDatabase = await _publisherRepository
            .GetPublisherWithCoursesAsync(getPublisherWithCoursesQuery.PublisherId);

        if (publisherFromDatabase == null) { 
            getPublisherWithCoursesQueryResponse.IsSuccessful = false;

            return getPublisherWithCoursesQueryResponse;
        }

        getPublisherWithCoursesQueryResponse.Publisher = _mapper
            .Map<GetPublisherWithCoursesDto>(publisherFromDatabase);
        
        return getPublisherWithCoursesQueryResponse;
    }
}

