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
        public PermissionRepository(
            AppDbContext context,
            IUnitOfWork unitOfWork,
            NotificationContext notificationContext
        ) : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public Permission GetByElement(FilterByItem filterByItem)
        {
            var (permission, includesValid) = GetElementEqual(filterByItem);

            if (!includesValid)
                return permission;

            if (filterByItem.Field == nameof(Permission.Id) && permission is null)
                _notificationContext.AddNotification("Permissão não encontrada.");

            return permission;
        }

        public FilterReturn<Permission> GetFilter(FilterPermissionTable filter)
        {
            var filters = new Dictionary<string, string>();

            TryAddFilter(filters, nameof(filter.ResourceContains), filter.ResourceContains);
            TryAddFilter(filters, nameof(filter.CanViewEqual), filter.CanViewEqual?.ToString());
            TryAddFilter(filters, nameof(filter.CanCreateEqual), filter.CanCreateEqual?.ToString());
            TryAddFilter(filters, nameof(filter.CanEditEqual), filter.CanEditEqual?.ToString());
            TryAddFilter(filters, nameof(filter.CanDeleteEqual), filter.CanDeleteEqual?.ToString());

            var (result, _) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);

            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, Permission existingPermission = null)
        {
            var isValid = true;
            dynamic permissionDto = dto;

            if (!IsEmailUnique(existingPermission, permissionDto.Email))
                isValid = false;

            return isValid;
        }

        private bool IsEmailUnique(object existingPermission, string email)
        {
            var emailInUse = GetByElement(new FilterByItem
            {
                Field = "Email",
                Value = email,
                Key = "Equal"
            }) is not null;

            var isSameAsExisting = existingPermission is not null &&
                                   ((dynamic)existingPermission).Email == email;

            if (!isSameAsExisting && emailInUse)
            {
                _notificationContext.AddNotification("Esse e-mail já está cadastrado.");
                return false;
            }

            return true;
        }

        private void TryAddFilter(Dictionary<string, string> filters, string key, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                filters[key] = value;
        }
    }
}
