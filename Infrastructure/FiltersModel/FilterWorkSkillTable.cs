namespace Infrastructure.FiltersModel
{
    public class FilterWorkSkillTable
    {
        public Guid? WorkIdGuid { get; set; }
        public Guid? SkillIdGuid { get; set; }
        public string[]? Includes { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
