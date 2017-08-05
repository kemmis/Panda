using System;

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
    }
}
