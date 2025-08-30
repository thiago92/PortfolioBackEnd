using Application.Dtos;
using Infrastructure.Notifications;

namespace Application.Specification;

public class PermissionSpecification
{
    private readonly NotificationContext _notificationContext;

    public PermissionSpecification(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public bool IsSatisfiedBy(object dto)
    {
        return dto switch
        {
            PermissionCreateDto createDto => ValidateCreateDto(createDto),
            PermissionUpdateDto updateDto => ValidateUpdateDto(updateDto),
            _ => false
        };
    }

    public bool ValidateCreateDto(PermissionCreateDto createDto)
    {
        return ValidateResource(createDto.Resource);
    }

    public bool ValidateUpdateDto(PermissionUpdateDto updateDto)
    {
        return ValidateId(updateDto.Id)
            && ValidateResource(updateDto.Resource);
    }

    private bool ValidateResource(string resource)
    {
        if (string.IsNullOrWhiteSpace(resource))
        {
            _notificationContext.AddNotification("Resource é obrigatório.");
            return false;
        }
        return true;
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
}
