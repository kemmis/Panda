using System;
using System.Collections.Generic;
using System.Text;

namespace PandaPress.Core.Models.Data
{
    public class BlogApplicationUser
    {
        public int BlogId { get; set; }
        public string ApplicationUserId { get; set; }
        public Blog Blog { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
