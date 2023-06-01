using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

internal class PersonalConfiguration : IEntityTypeConfiguration<Personal>
{
    public void Configure(EntityTypeBuilder<Personal> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.PersonalFullName).HasMaxLength(100);
        builder.Property(x => x.PersonalPosition).HasMaxLength(100);
        builder.Property(x => x.PersonalImageUrl).HasMaxLength(200);
    }
}