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
                .ForMember(dest => dest.UserDisplayName, opts => opts.MapFrom(src => src.User.DisplayName));
            CreateMap<ApplicationUser, ProfileSettingsViewModel>();
        }
    }
}
