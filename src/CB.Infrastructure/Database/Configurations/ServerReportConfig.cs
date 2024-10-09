using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class ServerReportConfig : IEntityTypeConfiguration<ServerReport> {

    public void Configure(EntityTypeBuilder<ServerReport> builder) {
        builder.ToTable(nameof(ServerReport));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32).IsRequired();

        builder.Property(o => o.UserBotId).HasMaxLength(32).IsRequired();
        builder.Property(o => o.MerchantId).HasMaxLength(32).IsRequired();

        // fk
        builder.HasOne(o => o.UserBot).WithMany(o => o.ServerReports).HasForeignKey(o => o.UserBotId);
    }
}
