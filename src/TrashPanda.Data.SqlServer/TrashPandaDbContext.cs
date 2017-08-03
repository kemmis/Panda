using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrashPanda.Core.Models.Data;

namespace TrashPanda.Data.SqlServer
{
    public class TrashPandaDbContext : DbContext
    {
       
        public TrashPandaDbContext(DbContextOptions<TrashPandaDbContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            base.OnConfiguring(optionsBuilder);
            
        }
        public DbSet<Post> Posts { get; set; }
    }
}
