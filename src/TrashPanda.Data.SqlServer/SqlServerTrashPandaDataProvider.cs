using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrashPanda.Core.Contracts;
using TrashPanda.Core.Models.Data;

namespace TrashPanda.Data.SqlServer
{
    public class SqlServerTrashPandaDataProvider : ScopedDataProviderBase, ITrashPandaDataProvider
    {
        public SqlServerTrashPandaDataProvider(ScopedDataProviderBaseDependencies baseDependencies) : base(baseDependencies)
        {
        }

        public Post GetPostBySlug(string slug)
        {
            using (ReadOnlyScope)
            {
                return TrashPandaDbContext.Posts.FirstOrDefault(p => p.Slug == slug);
            }
        }
    }
}
