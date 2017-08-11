using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public bool Removed { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }

        public Post Post { get; set; }
    }
}
