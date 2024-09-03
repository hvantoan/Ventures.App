using CB.Domain.Entities;
using CB.Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

public class MerchantConfig : IEntityTypeConfiguration<Merchant> {

    public void Configure(EntityTypeBuilder<Merchant> builder) {
        builder.ToTable(nameof(Merchant));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);

        builder.Property(o => o.Code).HasMaxLength(50).IsRequired();
        builder.Property(o => o.Name).HasMaxLength(255).IsRequired();
        builder.Property(o => o.SearchName).HasMaxLength(255).IsRequired();
        builder.Property(o => o.ApiSecret).HasMaxLength(255);
        builder.Property(o => o.ExpiredDate).HasDateConversion().IsRequired();
        builder.Property(o => o.CreatedDate).HasDateConversion().IsRequired();

        builder.Property(o => o.Province).HasMaxLength(20);
        builder.Property(o => o.District).HasMaxLength(20);
        builder.Property(o => o.Commune).HasMaxLength(20);
        builder.Property(o => o.Address).HasMaxLength(255);

        builder.HasIndex(o => o.Code).IsUnique();
    }
}
