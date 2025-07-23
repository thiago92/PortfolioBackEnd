using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Resource)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CanView)
                .IsRequired();

            builder.Property(p => p.CanCreate)
                .IsRequired();

            builder.Property(p => p.CanEdit)
                .IsRequired();

            builder.Property(p => p.CanDelete)
                .IsRequired();

            builder.HasMany(p => p.RolePermissions)
                .WithOne(rp => rp.Permission)
                .HasForeignKey(rp => rp.PermissionId);
        }
    }
}
