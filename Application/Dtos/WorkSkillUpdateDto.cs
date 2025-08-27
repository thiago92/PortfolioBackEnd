namespace Application.Dtos
{
    public class WorkSkillUpdateDto
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public Guid SkillId { get; set; }
    }
}
