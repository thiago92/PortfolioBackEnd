namespace Application.Dtos
{
    public class RoleReadDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<RolePermissionReadDto>? RolePermissions { get; set; }
    }
}
