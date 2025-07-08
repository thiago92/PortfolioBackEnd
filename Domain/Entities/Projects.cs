namespace Domain.Entities
{
    public class Projects
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Image { get; set; }
        public required string Url { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
