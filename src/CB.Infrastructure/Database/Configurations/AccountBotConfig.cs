using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class AccountBotConfig : IEntityTypeConfiguration<AccountBot> {

    public void Configure(EntityTypeBuilder<AccountBot> builder) {
        builder.ToTable(nameof(AccountBot));

        builder.HasKey(o => new { o.AccountId, o.BotId });
        builder.Property(o => o.AccountId).HasMaxLength(32).IsFixedLength();
        builder.Property(o => o.BotId).HasMaxLength(32).IsFixedLength();

        //fk
        builder.HasOne(o => o.Bot).WithMany(o => o.AccountBots).HasForeignKey(o => o.AccountId);
        builder.HasOne(o => o.Account).WithMany(o => o.AccountBots).HasForeignKey(o => o.AccountId);
    }
}
