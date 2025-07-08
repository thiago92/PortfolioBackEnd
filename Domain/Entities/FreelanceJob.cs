namespace Domain.Entities
{
    public class FreelanceJob
    {
        public Guid Id { get; set; }
        public required string Url { get; set; }
        public required string Image { get; set; }
        public required string AltImage { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
