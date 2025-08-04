using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;

namespace Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly NotificationContext _notificationContext;

        public RoleRepository(AppDbContext context, IUnitOfWork unitOfWork, NotificationContext notificationContext)
            : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public Role GetByElement(FilterByItem filterByItem)
        {
            var (role, hasIncludes) = GetElementEqual(filterByItem);

            if (hasIncludes)
                return role;

            if (filterByItem.Field == "Id" && role is null)
                _notificationContext.AddNotification("Perfil não encontrado.");

            return role;
        }

        public FilterReturn<Role> GetFilter(FilterRoleTable filter)
        {
            var filters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(filter.NameContains))
                filters[nameof(filter.NameContains)] = filter.NameContains;

            if (filter.CreatedDateBetweenDates.HasValue)
            {
                var date = filter.CreatedDateBetweenDates.Value;
                filters["StartDate"] = date.ToString("yyyy-MM-dd");
                filters["EndDate"] = date.ToString("yyyy-MM-dd");
            }

            var (result, _) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);

            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, Role existingRole = null)
        {
            var isValid = true;
            dynamic roleDto = dto;

            if (!IsEmailInUse(existingRole, roleDto.Email)) isValid = false;

            return isValid;
        }

        private bool IsEmailInUse(object? existingRole, string email)
        {
            if ((existingRole == null || ((dynamic)existingRole).Email != email) &&
                GetByElement(new FilterByItem { Field = "Email", Value = email, Key = "Equal" }) is not null)
            {
                _notificationContext.AddNotification("Esse email já está cadastrado.");
                return false;
            }
            return true;
        }
    }
}
