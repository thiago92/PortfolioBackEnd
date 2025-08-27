namespace Application.Dtos
{
    public class WorkUpdateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string Image { get; set; }
        public required string AltImage { get; set; }
        public bool IsFreelance { get; set; }
    }
}
