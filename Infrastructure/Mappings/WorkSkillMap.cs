using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Mappings
{
    public class WorkSkillMap : IEntityTypeConfiguration<WorkSkill>
    {
        public void Configure(EntityTypeBuilder<WorkSkill> builder)
        {
            
            builder.HasKey(ws => new { ws.WorkId, ws.SkillId });

            builder.HasOne(ws => ws.Work)
                .WithMany(w => w.WorkSkills)
                .HasForeignKey(ws => ws.WorkId);

            builder.HasOne(ws => ws.Skill)
                .WithMany(s => s.WorkSkills)
                .HasForeignKey(ws => ws.SkillId);
        }
    }
}
