using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Panda.Core.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string About { get; set; }
        public string ProfilePicture { get; set; }
        public List<Post> Posts { get; set; }
        public List<BlogApplicationUser> BlogApplicationUsers { get; set; }

    }
}
