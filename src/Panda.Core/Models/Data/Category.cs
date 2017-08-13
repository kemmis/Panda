using System.Collections.Generic;

namespace Panda.Core.Models.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    }
}
