using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Features.Authors.Commands;
using Univali.Api.Features.Authors.Commands.CreateAuthor;
using Univali.Api.Features.Authors.Commands.UpdateAuthor;
using Univali.Api.Features.Authors.Queries;
using Univali.Api.Models;

namespace Univali.Api.Profiles;

public class AuthorProfile : Profile 
{
    public AuthorProfile() 
    {
        CreateMap<UpdateAuthorCommand, Author>();

        CreateMap<Author, CreateAuthorDto>();
        
        CreateMap<CreateAuthorCommand, Author>();

        CreateMap<Author, GetAuthorWithCoursesDto>();

        CreateMap<Author, AuthorDto>().ReverseMap();

        CreateMap<Author, GetAuthorWithCoursesDto>();
    }
}

