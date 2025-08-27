namespace Application.Dtos
{
    public class SkillUpdateDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Value { get; set; }
    }
}
