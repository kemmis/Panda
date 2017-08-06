using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Data
{
    public class PostCategory
    {
        public Post Post { get; set; }
        public int PostId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
