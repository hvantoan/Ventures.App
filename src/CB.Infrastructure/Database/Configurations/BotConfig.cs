using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class BotConfig : IEntityTypeConfiguration<Bot> {

    public void Configure(EntityTypeBuilder<Bot> builder) {
        builder.ToTable(nameof(Bot));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);

        builder.Property(o => o.Name).HasMaxLength(255);
        builder.Property(o => o.Description).HasMaxLength(2000);

        //fk

        builder.HasMany(o => o.AccountBots).WithOne(o => o.Bot).HasForeignKey(o => o.BotId);

        builder.HasData(
            new Bot {
                Id = "ec0f270b424249438540a16e9157c0c8",
                Name = "CBV_SynthFX",
                Description = "Bot giao dịch tự động Forex",
            },
            new Bot {
                Id = "ec0f272b424249938540a16e9157c0c8",
                Name = "FX_Trader",
                Description = "Bot giao dịch cổ phiếu",
            }
        );
    }
}
