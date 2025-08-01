using Domain.Entities;
using Infrastructure.FiltersModel;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        Permission GetByElement(FilterByItem filterByItem);
        FilterReturn<Permission> GetFilter(FilterPermissionTable filter);
        bool ValidateInput(object dto, bool isUpdate, Permission? existingPermission = null);
    }
}
