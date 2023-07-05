using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

public class GetPublisherWithCoursesQuery 
    : IRequest<GetPublisherWithCoursesQueryResponse>
{
    public int PublisherId { get; set; }
}