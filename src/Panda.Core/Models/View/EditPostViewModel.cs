using System.Collections.Generic;

namespace Panda.Core.Models.View
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishDate { get; set; }
        public string Slug { get; set; }
        public bool Published { get; set; }
        public List<string> Categories { get; set; }
    }
}
