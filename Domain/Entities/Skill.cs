namespace Domain.Entities
{
    public class Skill
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public required string Label { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
