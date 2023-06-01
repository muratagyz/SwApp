using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.ProjectCategory).HasMaxLength(300);
        builder.Property(x => x.ProjectDetails).HasMaxLength(500);
        builder.Property(x => x.ProjectImageUrl).HasMaxLength(300);
        builder.Property(x => x.ProjectUrl).HasMaxLength(300);
    }
}