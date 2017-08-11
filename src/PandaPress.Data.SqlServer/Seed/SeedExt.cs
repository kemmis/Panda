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
        private PandaPressDbContext context;
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public DbInitializer(PandaPressDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task EnsureSeededAsync()
        {
            context.Database.EnsureCreated();

            // Look for any posts.
            if (context.Posts.Any())
            {
                return;   // DB has been seeded
            }

            #region create roles and first user

            string[] roleNames = { PandaPressRoles.Administrator, PandaPressRoles.Blogger };


            foreach (var roleName in roleNames)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var adminUser = new ApplicationUser
            {
                Email = "admin@pandapress.com",
                UserName = "admin",
                SecurityStamp = "pand-om-nom-nom-on-bamboo",
                DisplayName = "Administrator"
            };

            userManager.PasswordValidators.Clear();

            var result = await userManager.CreateAsync(adminUser, "admin");
            await userManager.AddToRoleAsync(adminUser, PandaPressRoles.Administrator);
            await userManager.AddToRoleAsync(adminUser, PandaPressRoles.Blogger);

            #endregion

            var defaultBlog = new Blog
            {
                Name = "Panda Blog",
                Description = "A Blogging System From The Future",
                PostsPerPage = 5
            };

            var blogUser = new BlogApplicationUser
            {
                Blog = defaultBlog,
                ApplicationUserId = adminUser.Id
            };

            defaultBlog.BlogApplicationUsers.Add(blogUser);

            await context.Blogs.AddAsync(defaultBlog);

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

            await context.Posts.AddRangeAsync(posts);

            await context.SaveChangesAsync();
        }
    }
}
