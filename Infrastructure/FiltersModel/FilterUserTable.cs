namespace Infrastructure.FiltersModel
{
    public class FilterUserTable
    {
        public string? UserNameContains { get; set; }
        public string? EmailContains { get; set; }
        public string? PasswordHashContains { get; set; }
        public DateTime? CreatedDateBetweenDates { get; set; } = DateTime.UtcNow;
        public Guid? RoleIdGuid { get; set; }
        public string[]? Includes { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
