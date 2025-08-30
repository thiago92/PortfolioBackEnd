using Application.Dtos;
using Infrastructure.Notifications;

namespace Application.Specification;

public class UserSpecification
{
    private readonly NotificationContext _notificationContext;

    public UserSpecification(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public bool IsSatisfiedBy(object dto)
    {
        return dto switch
        {
            UserCreateDto createDto => ValidateCreateDto(createDto),
            UserUpdateDto updateDto => ValidateUpdateDto(updateDto),
            _ => false
        };
    }

    public bool ValidateCreateDto(UserCreateDto createDto)
    {
        return ValidateUserName(createDto.UserName)
            && ValidateEmail(createDto.Email)
            && ValidatePassword(createDto.Password) // Fixed: use Password property instead of PasswordHash
            && ValidateRoleId(createDto.RoleId);
    }

    public bool ValidateUpdateDto(UserUpdateDto updateDto)
    {
        return ValidateId(updateDto.Id)
            && ValidateUserName(updateDto.UserName)
            && ValidateEmail(updateDto.Email)
            && ValidatePassword(updateDto.Password) // Fixed: use Password property instead of PasswordHash
            && ValidateRoleId(updateDto.RoleId);
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

    private bool ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            _notificationContext.AddNotification("Nome de usuário é obrigatório.");
            return false;
        }
        return true;
    }

    private bool ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            _notificationContext.AddNotification("Email é obrigatório.");
            return false;
        }
        else if (!email.Contains("@"))
        {
            _notificationContext.AddNotification("Email é inválido.");
            return false;
        }
        return true;
    }

    private bool ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            _notificationContext.AddNotification("Senha é obrigatória.");
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
}
