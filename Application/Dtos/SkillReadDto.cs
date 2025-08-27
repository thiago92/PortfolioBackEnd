namespace Application.Dtos
{
    public class SkillReadDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Value { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
