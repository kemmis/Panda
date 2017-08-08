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

        public Post CreatePost(string title, string content, string slug, string username, bool publish, int blogId)
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
            _db.SaveChanges();

            return post;
        }

        public void UpdatePost(int postId, string title, string content, bool publish)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                post.Title = title;
                post.Content = content;
                post.Published = publish;
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

        public Blog UpdateBlog(int blogId, string blogName)
        {
            var blog = _db.Blogs.FirstOrDefault(b => b.Id == blogId);
            if (blog != null)
            {
                blog.Name = blogName;
                _db.SaveChanges();
            }
            return blog;
        }

        public int GetNumPublishedPosts()
        {
            var blog = _db.Blogs.Include(b=>b.Posts).FirstOrDefault();
            if (blog != null)
            {
                return blog.Posts.Count(p => p.Published);
            }
            return 0;
        }

        public int GetNumDrafts()
        {
            var blog = _db.Blogs.Include(b=>b.Posts).FirstOrDefault();
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
            return _db.Categories.Include(c=>c.PostCategories).ToList();
        }
    }
}

