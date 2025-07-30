namespace Infrastructure.FiltersModel
{
    public class FilterRolePermissionTable
    {
        public Guid? RoleIdGuid { get; set; }
        public Guid? PermissionIdGuid { get; set; }
        public string[]? Includes { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
