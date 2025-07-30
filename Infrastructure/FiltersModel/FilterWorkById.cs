namespace Infrastructure.FiltersModel
{
    public class FilterWorkById
    {
        public Guid Id { get; set; }
        public string[]? Includes { get; set; }
    }
}
