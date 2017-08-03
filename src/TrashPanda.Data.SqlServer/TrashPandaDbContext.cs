using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.Data;

namespace TrashPanda.Data.SqlServer
{
    public class TrashPandaDbContext : DbContext, IDbContext
    {
        public TrashPandaDbContext(DbContextOptions options) : base(options) { }
        public TrashPandaDbContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=DESKTOP-116JPUG\SQLEXPRESS;Database=TrashPandaDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<Post> Posts { get; set; }
    }
}
