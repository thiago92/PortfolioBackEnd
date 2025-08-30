namespace Domain.Entities
{
    public class WorkSkill
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public Guid SkillId { get; set; }
        public virtual Skill Skill { get; set; }
        public virtual Work Work { get; set; }
    }
}
