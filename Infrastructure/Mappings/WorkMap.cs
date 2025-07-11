using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class WorkMap : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(w => w.Url)
                .IsRequired();

            builder.Property(w => w.Image)
                .IsRequired();
            builder.Property(w => w.AltImage)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(w => w.IsFreelance);

            builder.Property(w => w.CreatedDate)
                .IsRequired();

            builder.HasMany(w => w.WorkSkills)
                .WithOne(ws => ws.Work)
                .HasForeignKey(ws => ws.WorkId);
        }
    }
}
