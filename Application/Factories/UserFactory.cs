using Application.Dtos;
using Application.Interfaces.IFactories;
using Domain.Entities;

namespace Application.Factories;

public class UserFactory : IUserFactory
{
    // Criar uma nova entidade User com valores padrão
    public User CreateUser()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            UserName = string.Empty,
            Email = string.Empty,
            PasswordHash = string.Empty,
            CreatedDate = DateTime.UtcNow,
            RoleId = Guid.Empty,
            Role = null
        };
    }

    // Replace all occurrences of 'PasswordHash' with 'Password' for UserCreateDto

    public UserCreateDto CreateUserCreateDto()
    {
        return new UserCreateDto
        {
            UserName = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            RoleId = Guid.Empty
        };
    }

    // Fix: Remove references to PasswordHash in UserReadDto creation and mapping

    public UserReadDto CreateUserReadDto()
    {
        return new UserReadDto
        {
            Id = Guid.NewGuid(),
            UserName = string.Empty,
            Email = string.Empty,
            CreatedDate = DateTime.UtcNow,
            RoleId = Guid.Empty
        };
    }

    // Replace all references to PasswordHash with Password for UserUpdateDto

    public UserUpdateDto CreateUserUpdateDto()
    {
        return new UserUpdateDto
        {
            Id = Guid.NewGuid(),
            UserName = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            RoleId = Guid.Empty
        };
    }

    // Mapeamentos

    public User MapToUser(UserCreateDto dto)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = dto.Password, // Fixed: Use Password property from UserCreateDto
            CreatedDate = DateTime.UtcNow,
            RoleId = dto.RoleId
        };
    }

    public UserCreateDto MapToUserCreateDto(User entity)
    {
        return new UserCreateDto
        {
            UserName = entity.UserName,
            Email = entity.Email,
            Password = entity.PasswordHash,
            RoleId = entity.RoleId
        };
    }

    public UserReadDto MapToUserReadDto(User entity)
    {
        return new UserReadDto
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            CreatedDate = entity.CreatedDate,
            RoleId = entity.RoleId
        };
    }

    public UserUpdateDto MapToUserUpdateDto(User entity)
    {
        return new UserUpdateDto
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            Password = entity.PasswordHash,
            RoleId = entity.RoleId
        };
    }

    public User MapToUserFromUpdateDto(UserUpdateDto dto)
    {
        return new User
        {
            Id = dto.Id,
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = dto.Password,
            RoleId = dto.RoleId
        };
    }
}
