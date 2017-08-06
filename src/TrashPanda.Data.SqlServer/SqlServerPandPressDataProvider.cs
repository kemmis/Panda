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

        public (IEnumerable<Post> posts, int totalPosts) GetPosts(int pageSize, int pageIndex)
        {
            var totalPosts = _db.Posts.Count(p => p.Published);
            var posts = _db.Posts.Include(p => p.User).OrderByDescending(p => p.PublishDate).Skip(pageIndex * pageSize)
                .Take(pageSize).ToList();
            return (posts, totalPosts);
        }

        public async Task Init()
        {
            //_db.Database.Migrate(); // run migrations

            await _dbInitializer.EnsureSeededAsync();
        }
    }
}

