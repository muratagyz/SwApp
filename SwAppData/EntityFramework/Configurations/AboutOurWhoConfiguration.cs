using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class AboutOurWhoConfiguration : IEntityTypeConfiguration<AboutOurWho>
{
    public void Configure(EntityTypeBuilder<AboutOurWho> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.AboutOurWhoTitle).HasMaxLength(300);
        builder.Property(x => x.AboutOurWhoDetails).HasMaxLength(500);
    }
}