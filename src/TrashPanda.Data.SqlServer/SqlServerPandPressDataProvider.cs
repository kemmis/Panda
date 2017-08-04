using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrashPanda.Core.Contracts;
using TrashPanda.Core.Models.Data;
using TrashPanda.Data.SqlServer.Seed;

namespace TrashPanda.Data.SqlServer
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
                PandaPressDbContext.EnsureSeeded();
                scope.SaveChanges();
            }
        }
    }
}
