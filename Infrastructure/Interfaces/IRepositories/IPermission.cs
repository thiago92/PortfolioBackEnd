using Domain.Entities;
using Infrastructure.FiltersModel;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IPermission : IBaseRepository<Permission>
    {
        Permission GetByElement(FilterByItem filterByItem);
        FilterReturn<Permission> GetFilter(FilterCustomDetailsTable filter);
        bool ValidateInput(object dto, bool isUpdate, Permission existingPermission = null);
    }
}
