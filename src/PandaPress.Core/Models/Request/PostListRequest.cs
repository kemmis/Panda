using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Request
{
    public class PostListRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string CategorySlug { get; set; }
    }
}
