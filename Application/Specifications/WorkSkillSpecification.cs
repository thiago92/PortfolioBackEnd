using Application.Dtos;
using Infrastructure.Notifications;

namespace Application.Specification;

public class WorkSkillSpecification
{
    private readonly NotificationContext _notificationContext;

    public WorkSkillSpecification(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public bool IsSatisfiedBy(object dto)
    {
        return dto switch
        {
            WorkSkillCreateDto createDto => ValidateCreateDto(createDto),
            WorkSkillUpdateDto updateDto => ValidateUpdateDto(updateDto),
            _ => false
        };
    }

    public bool ValidateCreateDto(WorkSkillCreateDto createDto)
    {
        return ValidateWorkId(createDto.WorkId)
            && ValidateSkillId(createDto.SkillId);
    }

    public bool ValidateUpdateDto(WorkSkillUpdateDto updateDto)
    {
        return ValidateId(updateDto.Id)
            && ValidateWorkId(updateDto.WorkId)
            && ValidateSkillId(updateDto.SkillId);
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

    private bool ValidateWorkId(Guid workId)
    {
        if (workId == Guid.Empty)
        {
            _notificationContext.AddNotification("WorkId é obrigatório.");
            return false;
        }
        return true;
    }

    private bool ValidateSkillId(Guid skillId)
    {
        if (skillId == Guid.Empty)
        {
            _notificationContext.AddNotification("SkillId é obrigatório.");
            return false;
        }
        return true;
    }
}
