using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.ServiceTitle).HasMaxLength(100);
        builder.Property(x => x.ServiceDetails).HasMaxLength(700);
        builder.Property(x => x.ServiceImageUrl).HasMaxLength(300);
    }
}