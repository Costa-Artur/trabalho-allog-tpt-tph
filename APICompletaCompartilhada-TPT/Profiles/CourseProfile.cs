using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Features.Courses.Commands;
using Univali.Api.Features.Courses.Queries;
using Univali.Api.Models;

namespace Univali.Api.Profiles;

public class CourseProfile : Profile 
{
    public CourseProfile() 
    {
        CreateMap<UpdateCourseCommand, Course>();

        CreateMap<Course, CreateCourseDto>().ReverseMap();

        CreateMap<CreateCourseCommand, Course>();

        CreateMap<Course, GetCourseWithAuthorsDto>();

        CreateMap<Course, CourseDto>().ReverseMap();

        CreateMap<Course, CourseWithoutAuthorDto>();

        CreateMap<Publisher, PublisherWithoutCoursesDto>();

        CreateMap<Course, CreateCourseDto>()
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
    }
}

