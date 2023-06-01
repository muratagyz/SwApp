using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
{
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.OfficeLocationName).HasMaxLength(200);
        builder.Property(x => x.OfficeLocationNumber).HasMaxLength(100);
    }
}