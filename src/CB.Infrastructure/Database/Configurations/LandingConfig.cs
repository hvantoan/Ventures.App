using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class LandingConfig : IEntityTypeConfiguration<Landing> {

    public void Configure(EntityTypeBuilder<Landing> builder) {
        builder.ToTable(nameof(Landing));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);

        builder.Property(o => o.MerchantId).HasMaxLength(32).IsRequired();
        builder.Property(o => o.Value).HasMaxLength(Int32.MaxValue);

        // index
        builder.HasIndex(o => new { o.MerchantId, o.Type });
    }
}
