namespace Application.Dtos
{
    public class RolePermissionUpdateDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
