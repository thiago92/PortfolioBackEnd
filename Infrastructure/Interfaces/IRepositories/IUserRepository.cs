using Domain.Entities;
using Infrastructure.FiltersModel;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByElement(FilterByItem filterByItem);
        FilterReturn<User> GetFilter(FilterUserTable filter);
        bool ValidateInput(object dto, bool isUpdate, User? existingUser = null);
    }
}
