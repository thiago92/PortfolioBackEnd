namespace Domain.Entities
{
    public class RolePermission
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        // Add this navigation property to fix CS1061
        public virtual Permission Permission { get; set; }
    }
}
