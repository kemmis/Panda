using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.Data;

namespace TrashPanda.Data.SqlServer
{
    public class PandaPressDbContext : DbContext, IDbContext
    {
        public PandaPressDbContext(DbContextOptions options) : base(options) { }
        public PandaPressDbContext() : base() { }

        public DbSet<Post> Posts { get; set; }
    }
}
