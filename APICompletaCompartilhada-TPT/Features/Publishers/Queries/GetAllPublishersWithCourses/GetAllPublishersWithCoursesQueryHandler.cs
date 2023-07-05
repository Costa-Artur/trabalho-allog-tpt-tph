using AutoMapper;
using FluentValidation;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

public class GetAllPublishersWithCoursesQueryHandler : 
    IRequestHandler<GetAllPublishersWithCoursesQuery, GetAllPublishersWithCoursesQueryResponse>
{
    private readonly IPublisherRepository _publisherRepository;

    private readonly IMapper _mapper;

    public GetAllPublishersWithCoursesQueryHandler(
        IPublisherRepository publisherRepository, 
        IMapper mapper
    ) {
        _publisherRepository = publisherRepository;

        _mapper = mapper;
    }

    public async Task<GetAllPublishersWithCoursesQueryResponse> Handle(
        GetAllPublishersWithCoursesQuery getAllPublishersWithCoursesQuery, 
        CancellationToken cancellationToken
    ) {
        GetAllPublishersWithCoursesQueryResponse getAllPublishersWithCoursesQueryResponse = new();

        List<GetPublisherWithCoursesDto> publishersToReturn = new();

        var publishersFromDatabase = await _publisherRepository
            .GetAllPublishersWithCoursesAsync();

        if (publishersFromDatabase == null) { 
            getAllPublishersWithCoursesQueryResponse.IsSuccessful = false;

            return getAllPublishersWithCoursesQueryResponse;
        }

        foreach (Publisher publisher in publishersFromDatabase)
        {
            if(publisher is LegalPublisher legalPublisher)
            {
                var legalPublisherToReturn = _mapper.Map<LegalPublisherDto>(legalPublisher);
                publishersToReturn.Add(legalPublisherToReturn);
            }
            else if (publisher is NaturalPublisher naturalPublisher)
            {
                var naturalPublisherToReturn = _mapper.Map<NaturalPublisherDto>(naturalPublisher);
                publishersToReturn.Add(naturalPublisherToReturn);
            }
        }
        getAllPublishersWithCoursesQueryResponse.Publisher = publishersToReturn;
        return getAllPublishersWithCoursesQueryResponse;
    }
}

