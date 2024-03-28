using EventEchosAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventEchosAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Auth> Auths { get; set; }
    }
}
