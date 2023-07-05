using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Models;
using Univali.Api.Features.Publishers.Commands.CreatePublisher;
using Univali.Api.Features.Publishers.Commands.UpdatePublisher;
using Univali.Api.Features.Publishers.Queries.GetPublisher;

namespace Univali.Api.Profiles;

public class PublisherProfile : Profile 
{
    public PublisherProfile() 
    {
        CreateMap<CreatePublisherCommand, Publisher>();

        CreateMap<Publisher, CreatePublisherDto>();

        CreateMap<UpdatePublisherCommand, Publisher>();

        // CreateMap<Publisher, GetPublisherWithCoursesDto>();

        CreateMap<Publisher, PublisherWithoutCoursesDto>();

        CreateMap<PublisherWithoutCoursesDto, Publisher>();

        CreateMap<LegalPublisher, LegalPublisherDto>();

        CreateMap<NaturalPublisher, NaturalPublisherDto>();

        CreateMap<Publisher, GetPublisherWithCoursesDto>()
            .Include<LegalPublisher, LegalPublisherDto>()
            .Include<NaturalPublisher, NaturalPublisherDto>()
            .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.GetType().Name))
            .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses));

        CreateMap<LegalPublisher, LegalPublisherDto>()
            .ForMember(dest => dest.CNPJ, opt => opt.MapFrom(src => src.CNPJ));

        CreateMap<NaturalPublisher, NaturalPublisherDto>()
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF));
    }
}

