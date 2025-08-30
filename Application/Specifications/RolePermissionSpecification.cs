using Application.Dtos;
using Infrastructure.Notifications;

namespace Application.Specification;

public class RolePermissionSpecification
{
    private readonly NotificationContext _notificationContext;

    public RolePermissionSpecification(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public bool IsSatisfiedBy(object dto)
    {
        return dto switch
        {
            RolePermissionCreateDto createDto => ValidateCreateDto(createDto),
            RolePermissionUpdateDto updateDto => ValidateUpdateDto(updateDto),
            _ => false
        };
    }

    public bool ValidateCreateDto(RolePermissionCreateDto createDto)
    {
        return ValidateRoleId(createDto.RoleId)
            && ValidatePermissionId(createDto.PermissionId);
    }

    public bool ValidateUpdateDto(RolePermissionUpdateDto updateDto)
    {
        return ValidateId(updateDto.Id)
            && ValidateRoleId(updateDto.RoleId)
            && ValidatePermissionId(updateDto.PermissionId);
    }

    private bool ValidateId(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notificationContext.AddNotification("Id é obrigatório.");
            return false;
        }
        return true;
    }

    private bool ValidateRoleId(Guid roleId)
    {
        if (roleId == Guid.Empty)
        {
            _notificationContext.AddNotification("RoleId é obrigatório.");
            return false;
        }
        return true;
    }

    private bool ValidatePermissionId(Guid permissionId)
    {
        if (permissionId == Guid.Empty)
        {
            _notificationContext.AddNotification("PermissionId é obrigatório.");
            return false;
        }
        return true;
    }
}
