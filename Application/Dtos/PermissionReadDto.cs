namespace Application.Dtos
{
    public class PermissionReadDto
    {
        public Guid Id { get; set; }
        public required string Resource { get; set; }
        public bool CanView { get; set; }
        public bool CanCreate { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public ICollection<Guid> RolePermissionIds { get; set; } = new List<Guid>();
    }
}
