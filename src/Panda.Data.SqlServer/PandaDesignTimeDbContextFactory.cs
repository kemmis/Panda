using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Panda.Data.SqlServer
{
    public class PandaDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PandaDbContext>
    {
        public PandaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PandaDbContext>();
            builder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=Panda;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new PandaDbContext(builder.Options);
        }
    }
}
