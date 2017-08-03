using System;
using System.Collections.Generic;
using System.Text;

namespace TrashPanda.Core.Models.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
