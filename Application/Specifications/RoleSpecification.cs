using Application.Dtos;
using Infrastructure.Notifications;

namespace Application.Specification;

public class RoleSpecification
{
    private readonly NotificationContext _notificationContext;

    public RoleSpecification(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public bool IsSatisfiedBy(object dto)
    {
        return dto switch
        {
            RoleCreateDto createDto => ValidateCreateDto(createDto),
            RoleUpdateDto updateDto => ValidateUpdateDto(updateDto),
            _ => false
        };
    }

    public bool ValidateCreateDto(RoleCreateDto createDto)
    {
        return ValidateName(createDto.Name);
    }

    public bool ValidateUpdateDto(RoleUpdateDto updateDto)
    {
        return ValidateId(updateDto.Id)
            && ValidateName(updateDto.Name);
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

    private bool ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _notificationContext.AddNotification("Nome é obrigatório.");
            return false;
        }
        return true;
    }
}
