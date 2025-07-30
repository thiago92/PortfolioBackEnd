namespace Infrastructure.FiltersModel
{
    public class FilterPermissionTable
    {
        public string? ResourceContains { get; set; }
        public bool? CanViewEqual { get; set; }
        public bool? CanCreateEqual { get; set; }
        public bool? CanEditEqual { get; set; }
        public bool? CanDeleteEqual { get; set; }
        public string[]? Includes { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
