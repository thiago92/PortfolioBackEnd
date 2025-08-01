using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;

namespace Infrastructure.Repositories
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        private readonly NotificationContext _notificationContext;
        public PermissionRepository(AppDbContext context, IUnitOfWork unitOfWork, NotificationContext notificationContext) : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public Permission GetByElement(FilterByItem filterByItem)
        {
            (Permission permission, bool validadeIncludes) = GetElementEqual(filterByItem);

            if(validadeIncludes) return permission;

            if(filterByItem.Field == "Id" && permission is null) _notificationContext.AddNotification("Registro não encontrado");

            return permission;
        }

        public FilterReturn<Permission> GetFilter(FilterPermissionTable filter)
        {
            var filters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(filter.ResourceContains))
                filters.Add(nameof(filter.ResourceContains), filter.ResourceContains);

            if (filter.CanViewEqual.HasValue)
                filters.Add(nameof(filter.CanViewEqual), filter.CanViewEqual.Value.ToString());

            if (filter.CanCreateEqual.HasValue)
                filters.Add(nameof(filter.CanCreateEqual), filter.CanCreateEqual.Value.ToString());

            if (filter.CanEditEqual.HasValue)
                filters.Add(nameof(filter.CanEditEqual), filter.CanEditEqual.Value.ToString());

            if (filter.CanDeleteEqual.HasValue)
                filters.Add(nameof(filter.CanDeleteEqual), filter.CanDeleteEqual.Value.ToString());

            (var result, bool validadeIncludes) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);

            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, Permission existingPermission = null)
        {
            var isValid = true;
            dynamic permissionDto = dto;

            if (!IsEmailInUse(existingPermission, permissionDto.Email)) isValid = false;

            return isValid;
        }

        private bool IsEmailInUse(object? existingPermission, string email)
        {
            if ((existingPermission == null || ((dynamic)existingPermission).Email != email) &&
                GetByElement(new FilterByItem { Field = "Email", Value = email, Key = "Equal" }) is not null)
            {
                _notificationContext.AddNotification("Esse email já está cadastrado.");
                return false;
            }
            return true;
        }
    }
}
