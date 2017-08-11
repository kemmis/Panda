using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Data
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PostsPerPage { get; set; }
        public List<BlogApplicationUser> BlogApplicationUsers { get; set; } = new List<BlogApplicationUser>();
        public List<Post> Posts { get; set; }
    }
}
