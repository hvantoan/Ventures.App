using CB.Domain.Entities;
using CB.Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class UserBotConfig : IEntityTypeConfiguration<UserBot> {

    public void Configure(EntityTypeBuilder<UserBot> builder) {
        builder.ToTable(nameof(UserBot));

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasMaxLength(32).IsRequired();
        builder.Property(o => o.BotId).HasMaxLength(32).IsRequired();
        builder.Property(o => o.UserId).HasMaxLength(32).IsRequired();
        builder.Property(o => o.MerchantId).HasMaxLength(32).IsRequired();

        builder.Property(o => o.PassView).HasMaxLength(255);
        builder.Property(o => o.PassWeb).HasMaxLength(255);
        builder.Property(o => o.BrokerServer).HasMaxLength(255);
        builder.Property(o => o.CreateAt).HasDateConversion().IsRequired();

        builder.HasIndex(o => new { o.UserId, o.BotId });

        //fk
        builder.HasOne(o => o.Bot).WithMany(o => o.UserBots).HasForeignKey(o => o.BotId);
        builder.HasOne(o => o.User).WithMany(o => o.UserBots).HasForeignKey(o => o.UserId);
    }
}
