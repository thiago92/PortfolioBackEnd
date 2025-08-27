namespace Application.Dtos
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public Guid RoleId { get; set; }
    }
}
