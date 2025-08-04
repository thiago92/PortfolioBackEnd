namespace Infrastructure.FiltersModel
{
    public class FilterRoleTable
    {
        public string? NameContains { get; set; }
        public DateTime? CreatedDateBetweenDates { get; set; }
        public string[]? Includes { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
