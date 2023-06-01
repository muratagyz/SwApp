using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class AboutSubTableConfiguration : IEntityTypeConfiguration<AboutSubTable>
{
    public void Configure(EntityTypeBuilder<AboutSubTable> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.AboutSubTableTitle).HasMaxLength(300);
        builder.Property(x => x.AboutSubTableImageUrl).HasMaxLength(500);
        builder.Property(x => x.AboutSubTableDetails).HasMaxLength(500);
    }
}