using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace PandaPress.Core.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public List<Post> Posts { get; set; }
        public List<BlogApplicationUser> BlogApplicationUsers { get; set; }
        
    }
}
