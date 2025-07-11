namespace Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
