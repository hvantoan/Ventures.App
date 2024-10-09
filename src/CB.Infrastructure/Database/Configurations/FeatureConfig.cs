using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class FeatureConfig : IEntityTypeConfiguration<Feature> {

    public void Configure(EntityTypeBuilder<Feature> builder) {
        builder.ToTable(nameof(Feature));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32).IsFixedLength();
        builder.Property(o => o.PricingId).HasMaxLength(32).IsFixedLength();

        builder.Property(o => o.Content).HasMaxLength(2000).IsRequired();
    }
}
