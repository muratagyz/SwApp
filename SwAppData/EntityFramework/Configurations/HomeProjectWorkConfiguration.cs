using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class HomeProjectWorkConfiguration : IEntityTypeConfiguration<HomeProjectWork>
{
    public void Configure(EntityTypeBuilder<HomeProjectWork> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.HomeProjectWorkSubTitle).HasMaxLength(300);
        builder.Property(x => x.HomeProjectWorkSubDetails).HasMaxLength(300);
        builder.Property(x => x.HomeProjectWorkImageUrl).HasMaxLength(300);
    }
}