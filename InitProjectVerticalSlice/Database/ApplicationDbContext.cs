using EventEchosAPI.Entities.Users;
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
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }
        public DbSet<UserRolePermission> UserRolePermissions { get; set; }
    }
}
