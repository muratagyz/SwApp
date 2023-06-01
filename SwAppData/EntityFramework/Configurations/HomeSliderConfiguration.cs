using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class HomeSliderConfiguration : IEntityTypeConfiguration<HomeSlider>
{
    public void Configure(EntityTypeBuilder<HomeSlider> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.HomeSliderTitle).HasMaxLength(300);
        builder.Property(x => x.HomeSliderDetails).HasMaxLength(500);
        builder.Property(x => x.HomeSliderImageUrl).HasMaxLength(300);
    }
}