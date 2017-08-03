using System;
using System.Collections.Generic;
using System.Text;

namespace TrashPanda.Core.Models.View
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
