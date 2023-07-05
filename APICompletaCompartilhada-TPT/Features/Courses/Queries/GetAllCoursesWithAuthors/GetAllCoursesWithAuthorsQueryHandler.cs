using AutoMapper;
using MediatR;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Queries
{
    public class GetAllCoursesWithAuthorsQueryHandler : 
        IRequestHandler<GetAllCoursesWithAuthorsQuery, List<GetCourseWithAuthorsDto>?>
    {
        private readonly IPublisherRepository _publisherRepository;
        
        private readonly IMapper _mapper;

        public GetAllCoursesWithAuthorsQueryHandler(
            IPublisherRepository publisherRepository, 
            IMapper mapper
        ) {
            _publisherRepository = publisherRepository;
            
            _mapper = mapper;
        }

        public async Task<List<GetCourseWithAuthorsDto>?> Handle(
            GetAllCoursesWithAuthorsQuery getAllCoursesWithAuthorsQuery, 
            CancellationToken cancellationToken
        ) {
            var coursesFromDatabase = await _publisherRepository
                .GetAllCoursesWithAuthorsAsync(getAllCoursesWithAuthorsQuery.PublisherId);

            if (coursesFromDatabase == null) { return null; }

            var coursesToReturn = _mapper.Map<List<GetCourseWithAuthorsDto>>(coursesFromDatabase);
            
            return coursesToReturn;
        }
    }
}
