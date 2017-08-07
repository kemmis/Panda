using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PandaPress.Data.SqlServer
{
    public class PandaPressDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PandaPressDbContext>
    {
        public PandaPressDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PandaPressDbContext>();
            builder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=PandaPress;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new PandaPressDbContext(builder.Options);
        }
    }
}
