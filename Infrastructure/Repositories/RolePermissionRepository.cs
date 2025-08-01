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
        public RolePermissionRepository(AppDbContext context, IUnitOfWork unitOfWork, NotificationContext notificationContext) : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public RolePermission GetByElement(FilterByItem filterByItem)
        {
            (RolePermission rolePermission, bool validadeIncludes) = GetElementEqual(filterByItem);

            if (validadeIncludes) return rolePermission;

            if (filterByItem.Field == "Id" && rolePermission is null) _notificationContext.AddNotification("Registro não encontrado");

            return rolePermission;
        }

        public FilterReturn<RolePermission> GetFilter(FilterRolePermissionTable filter)
        {
            var filters = new Dictionary<string, string>();

            if (filter.PermissionIdGuid.HasValue)
                filters.Add(nameof(RolePermission.PermissionId), filter.PermissionIdGuid.ToString());

            if (filter.RoleIdGuid.HasValue)
                filters.Add(nameof(RolePermission.RoleId), filter.RoleIdGuid.ToString());

            (var result, bool validadeIncludes) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);

            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, RolePermission existingRolePermission = null)
        {
            var isValid = true;
            dynamic rolePermissionDto = dto;

            if (!IsEmailInUse(existingRolePermission, rolePermissionDto.Email)) isValid = false;

            return isValid;
        }

        private bool IsEmailInUse(object? existingRolePermission, string email)
        {
            if ((existingRolePermission == null || ((dynamic)existingRolePermission).Email != email) &&
                GetByElement(new FilterByItem { Field = "Email", Value = email, Key = "Equal" }) is not null)
            {
                _notificationContext.AddNotification("Esse email já está cadastrado.");
                return false;
            }
            return true;
        }
    }
}
