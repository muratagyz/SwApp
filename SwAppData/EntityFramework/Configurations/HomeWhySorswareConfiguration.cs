using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class HomeWhySorswareConfiguration : IEntityTypeConfiguration<HomeWhySorsware>
{
    public void Configure(EntityTypeBuilder<HomeWhySorsware> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.HomeWhySorswareSubTitle).HasMaxLength(300);
        builder.Property(x => x.HomeWhySorswareSubDetails).HasMaxLength(600);
        builder.Property(x => x.HomeWhySorswareSubImageUrl).HasMaxLength(300);
    }
}