using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrashPanda.Data.SqlServer
{
    public class PandaPressDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PandaPressDbContext>
    {
        public PandaPressDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PandaPressDbContext>();
            builder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=PandaPress;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new PandaPressDbContext(builder.Options);
        }
    }
}
