using System;
using System.Collections.Generic;

namespace PandaPress.Core.Models.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string Slug { get; set; }
        public bool Published { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
