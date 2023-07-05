using Univali.Api.Models;

namespace Univali.Api.Features.Publishers.Queries.GetPublisher;

    public class LegalPublisherDto : GetPublisherWithCoursesDto
    {
        public string CNPJ { get; set; } = string.Empty;

        public string PublisherType {get;set;} = string.Empty;
    }
