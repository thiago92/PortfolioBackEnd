namespace Application.Dtos
{
    public class WorkCreateDto
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string Image { get; set; }
        public required string AltImage { get; set; }
        public bool IsFreelance { get; set; } = false;
    }
}
