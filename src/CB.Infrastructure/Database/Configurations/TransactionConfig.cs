using CB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class TransactionConfig : IEntityTypeConfiguration<Transaction> {

    public void Configure(EntityTypeBuilder<Transaction> builder) {
        builder.ToTable(nameof(Transaction));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);
        builder.Property(o => o.UserBotId).HasMaxLength(32).IsFixedLength();

        //fk

        builder.HasOne(o => o.UserBot).WithMany(o => o.Transactions).HasForeignKey(o => o.UserBotId);
    }
}
