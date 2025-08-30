using Application.Dtos;
using Infrastructure.Notifications;

namespace Application.Specification;

public class SkillSpecification
{
    private readonly NotificationContext _notificationContext;

    public SkillSpecification(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public bool IsSatisfiedBy(object dto)
    {
        return dto switch
        {
            SkillCreateDto createDto => ValidateCreateDto(createDto),
            SkillUpdateDto updateDto => ValidateUpdateDto(updateDto),
            _ => false
        };
    }

    public bool ValidateCreateDto(SkillCreateDto createDto)
    {
        return ValidateName(createDto.Name)
            && ValidateValue(createDto.Value);
    }

    public bool ValidateUpdateDto(SkillUpdateDto updateDto)
    {
        return ValidateId(updateDto.Id)
            && ValidateName(updateDto.Name)
            && ValidateValue(updateDto.Value);
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

    private bool ValidateValue(int value)
    {
        if (value < 0)
        {
            _notificationContext.AddNotification("Valor deve ser positivo.");
            return false;
        }
        return true;
    }
}
