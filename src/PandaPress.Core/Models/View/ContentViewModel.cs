using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.View
{
    public class ContentViewModel
    {
        public List<PostContentViewModel> Posts { get; set; }
        public List<CategoryContentViewModel> Categories { get; set; }
    }

    public class PostContentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Published { get; set; }
        public string PublishDate { get; set; }
    }

    public class CategoryContentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumPosts { get; set; }
        public string Slug { get; set; }
    }
}
