using AutoMapper;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.View;

namespace PandaPress.Core.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(src => src.PublishDate.ToShortDateString()))
                .ForMember(dest => dest.UserDisplayName, opts => opts.MapFrom(src => src.User.DisplayName));
            CreateMap<ApplicationUser, ProfileSettingsViewModel>();
            CreateMap<Blog, SettingsViewModel>()
                .ForMember(dest => dest.BlogName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.BlogId, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dest => dest.PostsPerPage, opts => opts.MapFrom(src => src.PostsPerPage));
            CreateMap<Post, PostContentViewModel>()
                .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(src => src.PublishDate.ToShortDateString()));
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.CreatedDateTime, opts => opts.MapFrom(src => src.CreatedDateTime.ToShortDateString()));
            CreateMap<Category, CategoryContentViewModel>()
                .ForMember(dest => dest.NumPosts, opts => opts.MapFrom(src => src.PostCategories.Count));
        }
    }
}
