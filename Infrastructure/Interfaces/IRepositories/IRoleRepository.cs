using Domain.Entities;
using Infrastructure.FiltersModel;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Role GetByElement(FilterByItem filterByItem);
        FilterReturn<Role> GetFilter(FilterRoleTable filter);
        bool ValidateInput(object dto, bool isUpdate, Role? existingRole = null);
    }
}
