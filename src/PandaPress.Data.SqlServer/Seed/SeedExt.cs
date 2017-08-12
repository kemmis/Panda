using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PandaPress.Core.Models.Data;
using PandaPress.Core.Models.Enum;

namespace PandaPress.Data.SqlServer.Seed
{
    public class DbInitializer
    {
        private readonly PandaPressDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(PandaPressDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task EnsureSeededAsync()
        {
            // Look for any posts.
            if (_context.Posts.Any())
            {
                return;   // DB has been seeded
            }

            #region create roles and first user

            string[] roleNames = { PandaPressRoles.Administrator, PandaPressRoles.Blogger };


            foreach (var roleName in roleNames)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var adminUser = new ApplicationUser
            {
                Email = "admin@pandapress.com",
                UserName = "admin",
                SecurityStamp = "pand-om-nom-nom-on-bamboo",
                DisplayName = "Administrator",
                About = "Your fancy 'about me' description goes here. You can edit this via the user profile section in the admin panel."
            };

            _userManager.PasswordValidators.Clear(); // remove password requirements so we can add a super unsafe password 'admin'

            var result = await _userManager.CreateAsync(adminUser, "admin");
            await _userManager.AddToRoleAsync(adminUser, PandaPressRoles.Administrator);
            await _userManager.AddToRoleAsync(adminUser, PandaPressRoles.Blogger);

            #endregion

            var defaultBlog = new Blog
            {
                Name = "Panda Press",
                Description = "<A Blogging System From The Future />",
                PostsPerPage = 5    
            };

            var blogUser = new BlogApplicationUser
            {
                Blog = defaultBlog,
                ApplicationUserId = adminUser.Id
            };

            defaultBlog.BlogApplicationUsers.Add(blogUser);

            await _context.Blogs.AddAsync(defaultBlog);

            var posts = new Post[]
            {
                new Post
                {
                    Blog = defaultBlog,
                    Slug="derp-derp-derp",
                    Content="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    PublishDate= DateTime.Now,
                    Title="Node Blows My Mind!",
                    Published = true,
                    UserId = adminUser.Id
                }
            };

            await _context.Posts.AddRangeAsync(posts);

            await _context.SaveChangesAsync();
        }
    }
}
