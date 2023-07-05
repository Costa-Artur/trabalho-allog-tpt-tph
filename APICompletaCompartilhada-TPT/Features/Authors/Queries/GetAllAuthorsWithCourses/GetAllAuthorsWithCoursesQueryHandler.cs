using AutoMapper;
using MediatR;
using Univali.Api.Features.Authors.Queries;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Authors
{
    public class GetAllAuthorsWithCoursesQueryHandler : 
        IRequestHandler<GetAllAuthorsWithCoursesQuery, List<GetAuthorWithCoursesDto>?>
    {
        private readonly IPublisherRepository _publisherRepository;
        
        private readonly IMapper _mapper;

        public GetAllAuthorsWithCoursesQueryHandler(
            IPublisherRepository publisherRepository, 
            IMapper mapper
        ) {
            _publisherRepository = publisherRepository;
            
            _mapper = mapper;
        }

        public async Task<List<GetAuthorWithCoursesDto>?> Handle(
            GetAllAuthorsWithCoursesQuery getAllAuthorsWithCoursesQuery, 
            CancellationToken cancellationToken
        ) {
            var authorsFromDatabase = await _publisherRepository
                .GetAllAuthorsWithCoursesAsync();

            if (authorsFromDatabase == null) { return null; }

            var authorsWithCoursesDto = _mapper
                .Map<List<GetAuthorWithCoursesDto>>(authorsFromDatabase);
            
            return authorsWithCoursesDto;
        }
    }
}
