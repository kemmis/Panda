using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Panda.Core.Models.Data;
using Panda.Core.Models.Enum;

namespace Panda.Data.SqlServer.Seed
{
    public class DbInitializer
    {
        private readonly PandaDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(PandaDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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

            string[] roleNames = { PandaRoles.Administrator, PandaRoles.Blogger };


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
            await _userManager.AddToRoleAsync(adminUser, PandaRoles.Administrator);
            await _userManager.AddToRoleAsync(adminUser, PandaRoles.Blogger);

            #endregion

            var defaultBlog = new Blog
            {
                Name = "Panda",
                Description = "<A Blogging System From The Future />",
                PostsPerPage = 5,
                SendCommentEmail = false,
                SmtpHost = "",
                SmtpPort = "25",
                SmtpPassword = "",
                SmtpUseSsl = false,
                EmailPrefix = "Blog",
                SmtpUsername = ""
            };

            var blogUser = new BlogApplicationUser
            {
                Blog = defaultBlog,
                ApplicationUserId = adminUser.Id
            };

            defaultBlog.BlogApplicationUsers.Add(blogUser);

            await _context.Blogs.AddAsync(defaultBlog);

            var pandaCategory = new Category
            {
                Title = "Panda",
                Slug = "panda"
            };

            await _context.Categories.AddAsync(pandaCategory);

            var post =
                new Post
                {
                    Blog = defaultBlog,
                    Slug = "welcome-to-panda",
                    Content = @"<p>It looks like you got Panda up and running. Good job! Now it's time to make Panda your own.</p>
                            <h3>Login and Change Your Password</h3>
                            <p>First things first! Click on the Login button in the top-right corner of the screen. The default username is 'admin' and the default password is 'admin'. Use thoes credentials to login. You'll notice that after you login the button in the top-right corner of the screen now says 'Admin'. Clicking on that now opens the admin control panel. Change your password by clicking on the key icon towards the top of the control panel.</p>
                            <h3>Give Your Blog a Name</h3>
                            <p>Configuring Panda is easy. Give your blog a unique name by clicking on the ""Blog Settings"" button in the control panel. You can also give your blog a description. Both the name and description are displayed at the top of your blog, as well as in various other places on the blog. So make them good!</p>
                            <h3>Give Yourself a Name</h3>
                            <p>Now, you'll want to customize profile settings. Click on the profile icon towards the top of the control panel. Change the display name to something that identifies you as the blog author. The display name is used at the end of each post in the ""about the author"" section. You can also customize the ""about"" text and profile picture which are also both used in the ""about the author"" section. Finally, you'll want to update the email address in your profile. This email address is used for delivering notifications when people comments on your posts.It is not visible to the public.</p>
                            <h3>Other Things to Consider</h3>
                            <ul>
                            <li>In order to get emails when people comment on your pots, you'll want to configure the email settings. These settings can be found under the 'Email' tab in the 'Blog Settings' section in the control panel.&nbsp;</li>
                            <li>Panda is just a baby at this point. More features are coming soon! If you would like to help add features to Panda, get involved on our GitHub page, or tweet at<a href=""https://github.com/kemmis/Panda/issues"" target=""_blank"" rel=""noopener""> @PandaTheBlog</a>.</li>
                            <li>Your feedback is important to us.If you have issues or feature request, please leave them<a href=""https://github.com/kemmis/Panda/issues"" target=""_blank"" rel=""noopener""> here</a>.</li>
                            </ul>",
                    PublishDate = DateTime.Now,
                    Title = "Welcome to Panda",
                    Published = true,
                    UserId = adminUser.Id
                };


            await _context.Posts.AddAsync(post);

            var postCat = new PostCategory
            {
                Category = pandaCategory,
                Post = post
            };

            await _context.AddAsync(postCat);

            await _context.SaveChangesAsync();
        }
    }
}
