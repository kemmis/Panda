using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrashPanda.Data.SqlServer
{
    public class TrashPandaDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TrashPandaDbContext>
    {
        public TrashPandaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TrashPandaDbContext>();
            builder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=TrashPandaDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new TrashPandaDbContext(builder.Options);
        }
    }
}
