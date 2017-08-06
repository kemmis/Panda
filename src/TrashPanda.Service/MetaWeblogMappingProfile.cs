using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using cloudscribe.MetaWeblog.Models;
using PandaPress.Core.Models.Data;

namespace PandaPress.Service
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
        }
    }
}
