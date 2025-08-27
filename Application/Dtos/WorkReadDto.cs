namespace Application.Dtos
{
    public class WorkReadDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public required string Image { get; set; }
        public required string AltImage { get; set; }
        public bool IsFreelance { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<WorkSkillReadDto>? WorkSkills { get; set; } // opcional para exibir skills relacionadas
    }
}
