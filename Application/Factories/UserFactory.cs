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

    // Criar um UserCreateDto vazio
    public UserCreateDto CreateUserCreateDto()
    {
        return new UserCreateDto
        {
            UserName = string.Empty,
            Email = string.Empty,
            PasswordHash = string.Empty,
            RoleId = Guid.Empty
        };
    }

    // Criar um UserReadDto vazio
    public UserReadDto CreateUserReadDto()
    {
        return new UserReadDto
        {
            Id = Guid.NewGuid(),
            UserName = string.Empty,
            Email = string.Empty,
            PasswordHash = string.Empty,
            CreatedDate = DateTime.UtcNow,
            RoleId = Guid.Empty
        };
    }

    // Criar um UserUpdateDto vazio
    public UserUpdateDto CreateUserUpdateDto()
    {
        return new UserUpdateDto
        {
            Id = Guid.NewGuid(),
            UserName = string.Empty,
            Email = string.Empty,
            PasswordHash = string.Empty,
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
            PasswordHash = dto.PasswordHash,
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
            PasswordHash = entity.PasswordHash,
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
            PasswordHash = entity.PasswordHash,
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
            PasswordHash = entity.PasswordHash,
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
            PasswordHash = dto.PasswordHash,
            RoleId = dto.RoleId
        };
    }
}
