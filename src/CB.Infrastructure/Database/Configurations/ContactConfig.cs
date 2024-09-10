using CB.Domain.Entities;
using CB.Domain.Extentions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class ContactConfig : IEntityTypeConfiguration<Contact> {

    public void Configure(EntityTypeBuilder<Contact> builder) {
        builder.ToTable(nameof(Contact));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);

        builder.Property(o => o.Name).HasMaxLength(255);
        builder.Property(o => o.Email).HasMaxLength(255);
        builder.Property(o => o.Phone).HasMaxLength(20);
        builder.Property(o => o.Address).HasMaxLength(Int32.MaxValue);
        builder.Property(o => o.CreateAt).HasDateConversion().IsRequired();

        builder.HasOne(o => o.User).WithMany(o => o.Contacts).HasForeignKey(o => o.UserId);
    }
}
