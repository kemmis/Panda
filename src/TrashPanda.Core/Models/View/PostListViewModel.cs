using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.View;

namespace PandaPress.Core.Models.View
{
    public class PostListViewModel
    {
        public List<PostViewModel> Posts { get; set; }
        public int TotalPosts { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
