namespace Infrastructure.FiltersModel
{
    public class FilterWorkTable
    {
        public string? NameContains { get; set; }
        public string? UrlContains { get; set; }
        public string? ImageContains { get; set; }
        public string? AltImageContains { get; set; }
        public bool? IsFreelanceEqual { get; set; } = false;
        public DateTime? CreatedDateBetweenDates { get; set; }
        public string[]? Includes { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
