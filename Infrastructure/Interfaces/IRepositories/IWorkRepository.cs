using Domain.Entities;
using Infrastructure.FiltersModel;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IWorkRepository : IBaseRepository<Work>
    {
        Work GetByElement(FilterByItem filterByItem);
        FilterReturn<Work> GetFilter(FilterWorkTable filter);
        bool ValidateInput(object dto, bool isUpdate, Work? existingWork = null);
    }
}
