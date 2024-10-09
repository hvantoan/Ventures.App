using CB.Domain.Entities;
using CB.Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class BotConfig : IEntityTypeConfiguration<Bot> {

    public void Configure(EntityTypeBuilder<Bot> builder) {
        builder.ToTable(nameof(Bot));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32).IsFixedLength();
        builder.Property(o => o.MerchantId).HasMaxLength(32).IsFixedLength();

        builder.Property(o => o.Name).HasMaxLength(255);
        builder.Property(o => o.SearchName).HasMaxLength(255);
        builder.Property(o => o.Description).HasMaxLength(2000);
        builder.Property(o => o.CreatedAt).HasDateConversion().IsRequired();

        //fk

        builder.HasMany(o => o.UserBots).WithOne(o => o.Bot).HasForeignKey(o => o.BotId);
        builder.HasMany(o => o.BotReports).WithOne(o => o.Bot).HasForeignKey(o => o.BotId);
    }
}
