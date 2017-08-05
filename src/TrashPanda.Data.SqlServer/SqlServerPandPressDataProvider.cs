using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PandaPress.Core.Contracts;
using PandaPress.Core.Models.Data;
using PandaPress.Data.SqlServer.Seed;

namespace PandaPress.Data.SqlServer
{
    public class SqlServerPandaPressDataProvider : ScopedDataProviderBase, IPandaPressDataProvider
    {
        public SqlServerPandaPressDataProvider(ScopedDataProviderBaseDependencies baseDependencies) : base(baseDependencies)
        {
        }

        public Post GetPostBySlug(string slug)
        {
            using (ReadOnlyScope)
            {
                return PandaPressDbContext.Posts.FirstOrDefault(p => p.Slug == slug);
            }
        }

        public (IEnumerable<Post> posts, int totalPosts) GetPosts(int pageSize, int pageIndex)
        {
            using (ReadOnlyScope)
            {
                var totalPosts = PandaPressDbContext.Posts.Count(p => p.Published);
                var posts = PandaPressDbContext.Posts.OrderByDescending(p => p.PublishDate).Skip(pageIndex * pageSize)
                    .Take(pageSize).ToList();
                return (posts, totalPosts);
            }
        }

        public void Init()
        {
            using (var scope = Scope)
            {
                PandaPressDbContext.Database.Migrate(); // run migrations
                PandaPressDbContext.EnsureSeeded(); // add seed data
                scope.SaveChanges();
            }
        }
    }
}
