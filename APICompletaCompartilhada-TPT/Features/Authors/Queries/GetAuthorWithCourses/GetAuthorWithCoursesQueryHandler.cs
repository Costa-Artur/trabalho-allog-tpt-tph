using AutoMapper;
using MediatR;
using Univali.Api.Features.Authors.Queries;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Authors
{
    public class GetAuthorWithCoursesQueryHandler : 
        IRequestHandler<GetAuthorWithCoursesQuery, GetAuthorWithCoursesDto?>
    {
        private readonly IPublisherRepository _publisherRepository;
        
        private readonly IMapper _mapper;

        public GetAuthorWithCoursesQueryHandler(
            IPublisherRepository publisherRepository, 
            IMapper mapper
        ) {
            _publisherRepository = publisherRepository;

            _mapper = mapper;
        }

        public async Task<GetAuthorWithCoursesDto?> Handle(
            GetAuthorWithCoursesQuery getAuthorWithCoursesQuery, 
            CancellationToken cancellationToken
        ) {
            var authorFromDatabase = await _publisherRepository
                .GetAuthorWithCoursesAsync(getAuthorWithCoursesQuery.AuthorId);

            if (authorFromDatabase == null) { return null; }

            var authorWithCoursesDto = _mapper
                .Map<GetAuthorWithCoursesDto>(authorFromDatabase);
            
            return authorWithCoursesDto;
        }
    }
}
