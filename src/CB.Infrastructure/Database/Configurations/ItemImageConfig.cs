using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class ItemImageConfig : IEntityTypeConfiguration<ItemImage> {

    public void Configure(EntityTypeBuilder<ItemImage> builder) {
        builder.ToTable(nameof(ItemImage));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);

        builder.Property(o => o.MerchantId).HasMaxLength(32).IsRequired();
        builder.Property(o => o.ItemId).HasMaxLength(32).IsRequired();
        builder.Property(o => o.Name).HasMaxLength(255).IsRequired();
        builder.Property(o => o.ItemType).HasMaxLength(20);
        builder.Property(o => o.Image).HasMaxLength(8000);

        // index
        builder.HasIndex(o => new { o.MerchantId, o.ItemType });
    }
}
