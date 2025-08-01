using Domain.Entities;
using Infrastructure.FiltersModel;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IRolePermissionRepository : IBaseRepository<RolePermission>
    {
        RolePermission GetByElement(FilterByItem filterByItem);
        FilterReturn<RolePermission> GetFilter(FilterRolePermissionTable filter);
        bool ValidateInput(object dto, bool isUpdate, RolePermission? existingRolePermission = null);
    }
}
