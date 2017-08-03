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
       
        public TrashPandaDbContext(DbContextOptions<TrashPandaDbContext> options) : base(options)
        { }
                
        public DbSet<Post> Posts { get; set; }
    }
}
