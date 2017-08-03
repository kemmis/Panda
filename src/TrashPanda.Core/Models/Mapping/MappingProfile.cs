using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.Data;
using TrashPanda.Core.Models.View;

namespace TrashPanda.Core.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostViewModel>();
        }
    }
}
