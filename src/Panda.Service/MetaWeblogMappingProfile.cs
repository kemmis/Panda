using AutoMapper;
using cloudscribe.MetaWeblog.Models;
using Panda.Core.Models.Data;
using Panda.Core.Models.Request;
using Panda.Core.Models.View;

namespace Panda.Service
{
    public class MetaWeblogMappingProfile : Profile
    {
        public MetaWeblogMappingProfile()
        {
            CreateMap<Blog, BlogInfoStruct>()
                .ForMember(dest => dest.blogId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.blogName, opts => opts.MapFrom(src => src.Name));
            CreateMap<Category, CategoryStruct>()
                .ForMember(dest => dest.title, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.Id));
            CreateMap<PostStruct, PostCreateRequest>()
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.title))
                .ForMember(dest => dest.Content, opts => opts.MapFrom(src => src.description));
            CreateMap<PostViewModel, PostStruct>()
                .ForMember(dest => dest.postId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.description, opts => opts.MapFrom(src => src.Content))
                .ForMember(dest => dest.title, opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.postDate, opts => opts.MapFrom(src => src.PublishDate))
                .ForMember(dest => dest.slug, opts => opts.MapFrom(src => src.Slug))
                .ForMember(dest => dest.publish, opts => opts.MapFrom(src => src.Published));
            CreateMap<PostStruct, PostEditRequest>()
                .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.title))
                .ForMember(dest => dest.Content, opts => opts.MapFrom(src => src.description));
        }
    }
}
