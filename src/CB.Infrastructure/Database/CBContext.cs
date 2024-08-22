using System.Reflection;
using CB.Domain.Common;
using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Database {

    public partial class CBContext : DbContext {
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        public DbSet<User> Users => Set<User>();

        public CBContext() {
        }

        public CBContext(string connectionString) : base(GetOptions(connectionString)) {
        }

        public CBContext(DbContextOptions<CBContext> options) : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Server=database.hvantoan.io.vn;Port=5432;Database=postgres;Pooling=true;");
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
