using Univali.Api.Models;

namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

    public class NaturalPublisherDto : GetPublisherWithCoursesDto
    {
        public string CPF { get; set; } = string.Empty;

        public string PublisherType {get;set;} = string.Empty;
    }
