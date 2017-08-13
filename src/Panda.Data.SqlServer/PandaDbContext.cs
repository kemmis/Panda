using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Panda.Core.Models.Data;

namespace Panda.Data.SqlServer
{
    public class PandaDbContext : IdentityDbContext<ApplicationUser>
    {
        public PandaDbContext(DbContextOptions options) : base(options) { }
        public PandaDbContext() : base() { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // tell EF we have man-to-many relationships
            builder.Entity<BlogApplicationUser>()
                .HasKey(b => new { b.BlogId, b.ApplicationUserId });
            builder.Entity<BlogApplicationUser>().HasOne(b => b.Blog).WithMany(b => b.BlogApplicationUsers)
                .HasForeignKey(b => b.BlogId);
            builder.Entity<BlogApplicationUser>().HasOne(b => b.ApplicationUser).WithMany(b => b.BlogApplicationUsers)
                .HasForeignKey(b => b.ApplicationUserId);

            builder.Entity<PostCategory>()
                .HasKey(b => new { b.PostId, b.CategoryId });
            builder.Entity<PostCategory>().HasOne(b => b.Post).WithMany(p => p.PostCategories)
                .HasForeignKey(p => p.PostId);
            builder.Entity<PostCategory>().HasOne(b => b.Category).WithMany(b => b.PostCategories)
                .HasForeignKey(b => b.CategoryId);
        }
    }
}
