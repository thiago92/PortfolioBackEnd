using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Mappings
{
    public class SkillMap : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Value)
                .IsRequired();

            builder.Property(s => s.CreatedDate)
                .IsRequired();

            builder.HasMany(s => s.WorkSkills)
                .WithOne(ws => ws.Skill)
                .HasForeignKey(ws => ws.SkillId);
        }
    }
}
