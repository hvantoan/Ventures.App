using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class UserConfig : IEntityTypeConfiguration<User> {

    public void Configure(EntityTypeBuilder<User> builder) {
        builder.ToTable(nameof(User));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);
        builder.Property(o => o.MerchantId).HasMaxLength(32).IsRequired();
        builder.Property(o => o.RoleId).HasMaxLength(32);

        builder.Property(o => o.Username).IsRequired();
        builder.Property(o => o.Password).IsRequired();

        builder.Property(o => o.Name).HasMaxLength(255).IsRequired();
        builder.Property(o => o.SearchName).HasMaxLength(255).IsRequired();

        builder.Property(o => o.Province).HasMaxLength(20);
        builder.Property(o => o.District).HasMaxLength(20);
        builder.Property(o => o.Commune).HasMaxLength(20);
        builder.Property(o => o.Address).HasMaxLength(255);

        // index
        builder.HasIndex(o => o.MerchantId);
        builder.HasIndex(o => new { o.MerchantId, o.Username }).IsUnique();

        // fk
        builder.HasOne(o => o.Role).WithMany(o => o.Users).HasForeignKey(o => o.RoleId);

        builder.HasMany(o => o.Contacts).WithOne(o => o.User).HasForeignKey(o => o.UserId);
        builder.HasMany(o => o.BankCards).WithOne(o => o.User).HasForeignKey(o => o.UserId);
        builder.HasMany(o => o.Accounts).WithOne(o => o.User).HasForeignKey(o => o.UserId);
    }
}
