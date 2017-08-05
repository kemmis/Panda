using EntityFramework.DbContextScope.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PandaPress.Core.Models.Data;

namespace PandaPress.Data.SqlServer
{
    public class PandaPressDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public PandaPressDbContext(DbContextOptions options) : base(options) { }
        public PandaPressDbContext() : base() { }

        public DbSet<Post> Posts { get; set; }
    }
}
