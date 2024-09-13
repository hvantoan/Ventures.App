using System.Reflection;
using CB.Domain.Common;
using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Database {

    public partial class CBContext : DbContext {
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        public DbSet<Merchant> Merchants => Set<Merchant>();
        public DbSet<ItemImage> ItemImages => Set<ItemImage>();

        public DbSet<User> Users => Set<User>();
        public DbSet<BankCard> BankCards => Set<BankCard>();
        public DbSet<UserBot> UserBots => Set<UserBot>();

        public DbSet<Bot> Bots => Set<Bot>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        public DbSet<Pricing> Pricings => Set<Pricing>();
        public DbSet<Feature> Features => Set<Feature>();
        public DbSet<Contact> Contacts => Set<Contact>();

        public CBContext() {
        }

        public CBContext(string connectionString) : base(GetOptions(connectionString)) {
        }

        public CBContext(DbContextOptions<CBContext> options) : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Server=hvantoan.io.vn;Port=5432;Database=ventures;Pooling=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema(CBchema.Default);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static DbContextOptions GetOptions(string connectionString) {
            return new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;
        }
    }
}
