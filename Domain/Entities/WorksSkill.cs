namespace Domain.Entities
{
    public class WorkSkill
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public Work Work { get; set; } = null!;
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; } = null!;
    }
}
