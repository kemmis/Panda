using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Contracts;
using TrashPanda.Core.Models.View;

namespace TrashPanda.Service
{
    public class PostService : IPostService
    {
        public PostViewModel GetPostBySlug(string slug)
        {
            return new PostViewModel
            {
                Title = "Node Blows My Mind"
            };
        }
    }
}
