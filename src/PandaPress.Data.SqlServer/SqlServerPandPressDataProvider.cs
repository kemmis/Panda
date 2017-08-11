using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Data;
using PandaPress.Data.SqlServer.Seed;

namespace PandaPress.Data.SqlServer
{
    public class SqlServerPandaPressDataProvider : IPandaPressDataProvider
    {

        private readonly PandaPressDbContext _db;
        private readonly DbInitializer _dbInitializer;

        public SqlServerPandaPressDataProvider(PandaPressDbContext pandaPressDbContext, DbInitializer dbInitializer)
        {
            _db = pandaPressDbContext;
            _dbInitializer = dbInitializer;
        }

        public IEnumerable<Blog> GetBlogsForUser(string username)
        {
            return _db.Blogs.Where(b =>
                b.BlogApplicationUsers.Any(ba => String.Equals(ba.ApplicationUser.UserName, username, StringComparison.CurrentCultureIgnoreCase)));
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }

        public Post GetPostBySlug(string slug)
        {
            return _db.Posts.Include(p => p.User).FirstOrDefault(p => p.Slug == slug);
        }

        public Post GetPostById(int postId)
        {
            return _db.Posts.Include(p => p.User).FirstOrDefault(p => p.Id == postId);
        }

        public (IEnumerable<Post> posts, int totalPosts) GetPosts(int pageSize, int pageIndex)
        {
            var totalPosts = _db.Posts.Count(p => p.Published);
            var posts = _db.Posts.Include(p => p.User).OrderByDescending(p => p.PublishDate).Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();
            return (posts, totalPosts);
        }

        public Post CreatePost(string title, string content, List<string> categories, string slug, string username, bool publish, int blogId)
        {
            var user = _db.Users.FirstOrDefault(u => String.Equals(u.UserName, username, StringComparison.CurrentCultureIgnoreCase));
            var blog = _db.Blogs.FirstOrDefault(b => b.Id == blogId);

            var post = new Post
            {
                Blog = blog,
                User = user,
                Title = title,
                Content = content,
                Published = publish,
                PublishDate = DateTime.Now,
                Slug = slug
            };

            _db.Posts.Add(post);

            foreach (var category in categories)
            {
                var catToAdd = _db.Categories.FirstOrDefault(c => String.Equals(c.Title, category, StringComparison.CurrentCultureIgnoreCase));
                if (catToAdd != null)
                {
                    post.PostCategories.Add(new PostCategory
                    {
                        Category = catToAdd,
                        Post = post
                    });
                }
            }

            _db.SaveChanges();

            return post;
        }

        public void UpdatePost(int postId, string title, string content, List<string> categories, bool publish)
        {
            var post = _db.Posts.Include(p => p.PostCategories).ThenInclude(pc => pc.Category).FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                post.Title = title;
                post.Content = content;
                post.Published = publish;

                var catsToRemove = post.PostCategories.Where(pc =>
                    !categories.Exists(c =>
                        String.Equals(c, pc.Category.Title, StringComparison.CurrentCultureIgnoreCase))).ToList();
                var catsToAdd = categories.Where(c => post.PostCategories.All(pc =>
                    !String.Equals(pc.Category.Title, c, StringComparison.CurrentCultureIgnoreCase))).ToList();

                foreach (var c in catsToRemove)
                {
                    post.PostCategories.Remove(c);
                }

                foreach (var c in catsToAdd)
                {
                    var catToAdd = _db.Categories.FirstOrDefault(c2 => String.Equals(c2.Title, c, StringComparison.CurrentCultureIgnoreCase));
                    if (catToAdd != null)
                    {
                        post.PostCategories.Add(new PostCategory
                        {
                            Category = catToAdd,
                            Post = post
                        });
                    }
                }

                _db.SaveChanges();
            }
        }
        public async Task Init()
        {
            //_db.Database.Migrate(); // run migrations

            await _dbInitializer.EnsureSeededAsync();
        }

        public Blog GetBlog()
        {
            return _db.Blogs.FirstOrDefault();
        }

        public Blog UpdateBlog(int blogId, string blogName, string description, int postsPerPage)
        {
            var blog = _db.Blogs.FirstOrDefault(b => b.Id == blogId);
            if (blog != null)
            {
                blog.Name = blogName;
                blog.Description = description;
                blog.PostsPerPage = postsPerPage;
                _db.SaveChanges();
            }
            return blog;
        }

        public int GetNumPublishedPosts()
        {
            var blog = _db.Blogs.Include(b => b.Posts).FirstOrDefault();
            if (blog != null)
            {
                return blog.Posts.Count(p => p.Published);
            }
            return 0;
        }

        public int GetNumDrafts()
        {
            var blog = _db.Blogs.Include(b => b.Posts).FirstOrDefault();
            if (blog != null)
            {
                return blog.Posts.Count(p => !p.Published);
            }
            return 0;
        }

        public IEnumerable<Post> GetPosts()
        {
            return _db.Posts.ToList();
        }

        public IEnumerable<Category> GetCategoriesWithPostCategories()
        {
            return _db.Categories.Include(c => c.PostCategories).ToList();
        }

        public Category AddCategory(string title, string description)
        {
            var newCategory = new Category
            {
                Title = title,
                Description = description
            };
            _db.Categories.Add(newCategory);
            _db.SaveChanges();
            return newCategory;
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _db.Users.FirstOrDefault(u => u.Id == userId);
        }

        public ApplicationUser UpdateUser(string userId, string displayName)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.DisplayName = displayName;
                _db.SaveChanges();
            }
            return user;
        }
    }
}

