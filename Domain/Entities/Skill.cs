namespace Domain.Entities
{
    public class Skill
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<WorkSkill> WorkSkills { get; set; } = new List<WorkSkill>();

    }
}
