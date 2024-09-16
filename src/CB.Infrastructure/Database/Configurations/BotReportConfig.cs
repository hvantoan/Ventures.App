using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class BotReportConfig : IEntityTypeConfiguration<BotReport> {

    public void Configure(EntityTypeBuilder<BotReport> builder) {
        builder.ToTable(nameof(BotReport));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32).IsFixedLength();
        builder.Property(o => o.BotId).HasMaxLength(32).IsFixedLength();
        builder.Property(o => o.MerchantId).HasMaxLength(32).IsFixedLength();

        //fk

        builder.HasIndex(o => new { o.Month, o.Year, o.BotId }).IsUnique();

        builder.HasOne(o => o.Bot).WithMany(o => o.BotReports).HasForeignKey(o => o.BotId);
    }
}
