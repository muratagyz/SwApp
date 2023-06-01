using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class ContactInformationConfiguration : IEntityTypeConfiguration<ContactInformation>
{
    public void Configure(EntityTypeBuilder<ContactInformation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.ContactInformationTitle).HasMaxLength(300);
    }
}