using Ap104.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ap104.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired().HasMaxLength(30);

            base.OnModelCreating(modelBuilder);
        }
    }
}
