namespace Application.Dtos
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; } // opcional, para facilitar exibição
    }
}
