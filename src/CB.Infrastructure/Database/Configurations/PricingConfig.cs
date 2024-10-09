using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class PricingConfig : IEntityTypeConfiguration<Pricing> {

    public void Configure(EntityTypeBuilder<Pricing> builder) {
        builder.ToTable(nameof(Pricing));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32).IsFixedLength();

        builder.Property(o => o.MonetaryUnit).HasMaxLength(Int32.MaxValue);

        // FK   
        builder.HasMany(o => o.Features).WithOne(o => o.Pricing).HasForeignKey(o => o.PricingId);
    }
}
