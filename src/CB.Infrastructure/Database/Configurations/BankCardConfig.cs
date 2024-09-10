using CB.Domain.Entities;
using CB.Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class BankCardConfig : IEntityTypeConfiguration<BankCard> {

    public void Configure(EntityTypeBuilder<BankCard> builder) {
        builder.ToTable(nameof(BankCard));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);

        builder.Property(o => o.Name).HasMaxLength(255);
        builder.Property(o => o.CardNumber).HasMaxLength(255);
        builder.Property(o => o.CardBranch).HasMaxLength(255);
        builder.Property(o => o.Cvv).HasMaxLength(5);
        builder.Property(o => o.CreatedAt).HasDateConversion().IsRequired();

        //fk
        builder.HasOne(o => o.User).WithMany(o => o.BankCards).HasForeignKey(o => o.UserId);
    }
}
