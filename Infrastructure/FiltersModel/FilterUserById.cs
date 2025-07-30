namespace Infrastructure.FiltersModel
{
    public class FilterUserById
    {
        public Guid Id { get; set; }
        public string[]? Includes { get; set; }
    }
}
