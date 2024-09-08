using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class AccountConfig : IEntityTypeConfiguration<Account> {

    public void Configure(EntityTypeBuilder<Account> builder) {
        builder.ToTable(nameof(Account));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32).IsFixedLength();
        builder.Property(o => o.UserId).HasMaxLength(32).IsFixedLength();

        builder.Property(o => o.BrokerServer).HasMaxLength(255);
        builder.Property(o => o.PassView).HasMaxLength(255);
        builder.Property(o => o.PassWeb).HasMaxLength(255);

        builder.HasOne(o => o.User).WithMany(o => o.Accounts).HasForeignKey(o => o.UserId);
        builder.HasMany(o => o.AccountBots).WithOne(o => o.Account).HasForeignKey(o => o.AccountId);
        builder.HasMany(o => o.Transactions).WithOne(o => o.Account).HasForeignKey(o => o.AccountId);
    }
}
