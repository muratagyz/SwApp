using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class HomeWhatAreWeDoingConfiguration : IEntityTypeConfiguration<HomeWhatAreWeDoing>
{
    public void Configure(EntityTypeBuilder<HomeWhatAreWeDoing> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.HomeWhatAreWeDoingTİtle).HasMaxLength(300);
    }
}