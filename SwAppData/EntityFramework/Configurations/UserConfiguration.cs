using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwAppData.Entity;

namespace SwAppData.EntityFramework.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.UserName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
        builder.Property(x => x.Password).HasMaxLength(50);
    }
}