namespace Domain.Entities
{
    public class Work
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string Image { get; set; }
        public required string AltImage { get; set; }
        public bool IsFreelance { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual ICollection<WorkSkill>? WorkSkills { get; set; }

    }
}
