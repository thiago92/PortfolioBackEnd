using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;

namespace Infrastructure.Repositories
{
    public class RolePermissionRepository : BaseRepository<RolePermission>, IRolePermissionRepository
    {
        private readonly NotificationContext _notificationContext;

        public RolePermissionRepository(
            AppDbContext context,
            IUnitOfWork unitOfWork,
            NotificationContext notificationContext
        ) : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public RolePermission GetByElement(FilterByItem filterByItem)
        {
            var (rolePermission, includesValid) = GetElementEqual(filterByItem);

            if (!includesValid && filterByItem.Field == nameof(RolePermission.Id) && rolePermission is null)
            {
                _notificationContext.AddNotification("Registro não encontrado.");
            }

            return rolePermission;
        }

        public FilterReturn<RolePermission> GetFilter(FilterRolePermissionTable filter)
        {
            var filters = new Dictionary<string, string>();

            TryAddFilter(filters, nameof(RolePermission.PermissionId), filter.PermissionIdGuid?.ToString());
            TryAddFilter(filters, nameof(RolePermission.RoleId), filter.RoleIdGuid?.ToString());

            var (result, _) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);
            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, RolePermission? existingRolePermission = null)
        {
            return true;
        }

        private void TryAddFilter(Dictionary<string, string> filters, string key, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                filters[key] = value;
        }
    }
}
