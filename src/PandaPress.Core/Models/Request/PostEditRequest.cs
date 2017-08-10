using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Request
{
    public class PostEditRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public string Username { get; set; }
        public bool Publish { get; set; }
        public List<string> Categories { get; set; }
    }
}
