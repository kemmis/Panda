using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.View
{
    public class PostListViewModel
    {
        public List<PostViewModel> Posts { get; set; }
        public int TotalPosts { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string PageTitle { get; set; }
    }
}
