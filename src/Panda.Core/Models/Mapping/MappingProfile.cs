using System.Linq;
using AutoMapper;
using Panda.Core.Models.Data;
using Panda.Core.Models.View;

namespace Panda.Core.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.PublishDate, opts => opts.MapFrom(src => src.PublishDate.ToShortDateString()))
                .ForMember(dest => dest.UserDisplayName, opts => opts.MapFrom(src => src.User.DisplayName))
                .ForMember(dest => dest.Categories,
                    opts => opts.MapFrom(src => src.PostCategories.Select(pc => pc.Category)))
                .ForMember(dest => dest.UserAbout, opts => opts.MapFrom(src => src.User.About))
                .ForMember(dest => dest.ProfilePicture, opts => opts.MapFrom(src => src.User.ProfilePicture));
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
            CreateMap<Blog, HomeViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Post, EditPostViewModel>()
                .ForMember(dest => dest.Categories,
                    opts => opts.MapFrom(src => src.PostCategories.Select(pc => pc.Category.Title)));
            CreateMap<Comment, DashboardDataCommentViewModel>()
                .ForMember(dest => dest.PostId, opts => opts.MapFrom(src => src.PostId))
                .ForMember(dest => dest.PostTitle, opts => opts.MapFrom(src => src.Post.Title))
                .ForMember(dest => dest.PostSlug, opts => opts.MapFrom(src => src.Post.Slug))
                .ForMember(dest => dest.CreatedDateTime, opts => opts.MapFrom(src => src.CreatedDateTime.ToShortDateString()));

        }
    }
}
