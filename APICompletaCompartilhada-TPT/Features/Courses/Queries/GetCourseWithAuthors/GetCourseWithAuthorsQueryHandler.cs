using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Queries
{
    public class GetCourseWithAuthorsQueryHandler : 
        IRequestHandler<GetCourseWithAuthorsQuery, GetCourseWithAuthorsDto?>
    {
        private readonly IPublisherRepository _publisherRepository;
        
        private readonly IMapper _mapper;

        public GetCourseWithAuthorsQueryHandler(
            IPublisherRepository publisherRepository, 
            IMapper mapper
        ) {
            _publisherRepository = publisherRepository;

            _mapper = mapper;
        }

        public async Task<GetCourseWithAuthorsDto?> Handle(
            GetCourseWithAuthorsQuery getCourseWithAuthorsQuery, 
            CancellationToken cancellationToken
        ) {
            var courseFromDatabase = await _publisherRepository
                .GetCourseWithAuthorsAsync(
                    getCourseWithAuthorsQuery.PublisherId, 
                    getCourseWithAuthorsQuery.CourseId
                );

            if (courseFromDatabase == null) { return null; }

            var coursesToReturn = _mapper.Map<GetCourseWithAuthorsDto>(courseFromDatabase);
            
            return coursesToReturn;
        }
    }
}
