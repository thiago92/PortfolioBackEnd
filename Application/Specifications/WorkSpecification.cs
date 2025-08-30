using Application.Dtos;
using Infrastructure.Notifications;

namespace Application.Specification;

public class WorkSpecification
{
    private readonly NotificationContext _notificationContext;

    public WorkSpecification(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public bool IsSatisfiedBy(object dto)
    {
        return dto switch
        {
            WorkCreateDto createDto => ValidateCreateDto(createDto),
            WorkUpdateDto updateDto => ValidateUpdateDto(updateDto),
            _ => false
        };
    }

    public bool ValidateCreateDto(WorkCreateDto createDto)
    {
        return ValidateName(createDto.Name)
            && ValidateUrl(createDto.Url)
            && ValidateImage(createDto.Image)
            && ValidateAltImage(createDto.AltImage);
    }

    public bool ValidateUpdateDto(WorkUpdateDto updateDto)
    {
        return ValidateId(updateDto.Id)
            && ValidateName(updateDto.Name)
            && ValidateUrl(updateDto.Url)
            && ValidateImage(updateDto.Image)
            && ValidateAltImage(updateDto.AltImage);
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

    private bool ValidateUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            _notificationContext.AddNotification("URL é obrigatória.");
            return false;
        }
        return true;
    }

    private bool ValidateImage(string image)
    {
        if (string.IsNullOrWhiteSpace(image))
        {
            _notificationContext.AddNotification("Imagem é obrigatória.");
            return false;
        }
        return true;
    }

    private bool ValidateAltImage(string altImage)
    {
        if (string.IsNullOrWhiteSpace(altImage))
        {
            _notificationContext.AddNotification("Texto alternativo da imagem é obrigatório.");
            return false;
        }
        return true;
    }
}
