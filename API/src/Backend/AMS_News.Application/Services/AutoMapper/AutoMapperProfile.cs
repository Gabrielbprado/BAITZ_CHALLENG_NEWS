using AMS_News.Communication.Request.News;
using AMS_News.Communication.Request.User;
using AMS_News.Communication.Response.News;
using AMS_News.Communication.Response.User;
using AMS_News.Domain.Entities;
using AutoMapper;

namespace AMS_News.Application.Services.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RequestRegisterUserJson,Customers>();
        CreateMap<Customers,RequestRegisterUserJson>();
        CreateMap<Customers, ResponseUserProfileJson>();
        CreateMap<News, ResponseNewsJson>();
        CreateMap<News, ResponseRegiteredNewsJson>();
        CreateMap<News, ResponseNewsJson>()
            .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics));

        CreateMap<Topics, ResponseTopicsJson>();
        CreateMap<RequestNewsJson, News>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Introduction, opt => opt.MapFrom(src => src.Introduction))
            .ForMember(dest => dest.Topics, opt => opt.Ignore());
        CreateMap<RequestTopicsJson, Topics>();

    }    
}