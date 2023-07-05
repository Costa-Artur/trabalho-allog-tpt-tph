using MediatR;

namespace Univali.Api.Features.Authors.Queries;

public class GetAllAuthorsWithCoursesQuery : IRequest<List<GetAuthorWithCoursesDto>?>
{
}