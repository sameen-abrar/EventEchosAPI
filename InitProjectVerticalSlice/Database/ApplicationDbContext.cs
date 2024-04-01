using EventEchosAPI.Entities.Events;
using EventEchosAPI.Entities.Orders;
using EventEchosAPI.Entities.Products;
using EventEchosAPI.Entities.Roles;
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

            modelBuilder.Entity<Coupon>()
            .Property(c => c.DiscountPercentage)
            .HasColumnType("decimal(18, 2)");
        }

        #region User
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }
        public DbSet<UserRolePermission> UserRolePermissions { get; set; }
        #endregion

        #region Roles
        public DbSet<Admin> Admins { get; set; }
        public DbSet<EventCoodrinator> EventCoodrinatorsEvents { get; set; }
        public DbSet<Guest> Guests { get; set; }
        #endregion

        #region Event
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCoodrinator> EventCoodrinators { get; set; }
        public DbSet<EventGuestImage> EventGuestImages { get; set; }
        public DbSet<ImageData> ImageDatas { get; set; }
        #endregion

        #region Order
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails  { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        #endregion

        #region Product
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVersion> ProductVersions { get; set; }
        #endregion

    }
}
