namespace Infrastructure.FiltersModel
{
    public class FilterPermissionById
    {
        public Guid Id { get; set; }
        public string[]? Includes { get; set; }
    }
}
