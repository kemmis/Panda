using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.View
{
    public class CommentViewModel
    {
        public string AuthorName { get; set; }
        public string Text { get; set; }
        public string CreatedDateTime { get; set; }
        public int Id { get; set; }
        public bool Removed { get; set; }
    }
}
