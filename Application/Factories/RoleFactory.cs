using Application.Dtos;
using Application.Interfaces.IFactories;
using Domain.Entities;

namespace Application.Factories;

public class RoleFactory : IRoleFactory
{
    // Criar uma nova entidade Role com valores padrão
    public Role CreateRole()
    {
        return new Role
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            CreatedDate = DateTime.UtcNow
        };
    }

    // Criar um RoleCreateDto vazio
    public RoleCreateDto CreateRoleCreateDto()
    {
        return new RoleCreateDto
        {
            Name = string.Empty
        };
    }

    // Criar um RoleReadDto vazio
    public RoleReadDto CreateRoleReadDto()
    {
        return new RoleReadDto
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            CreatedDate = DateTime.UtcNow
        };
    }

    // Criar um RoleUpdateDto vazio
    public RoleUpdateDto CreateRoleUpdateDto()
    {
        return new RoleUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = string.Empty
        };
    }

    // Mapeamentos

    public Role MapToRole(RoleCreateDto dto)
    {
        return new Role
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CreatedDate = DateTime.UtcNow
        };
    }

    public RoleCreateDto MapToRoleCreateDto(Role entity)
    {
        return new RoleCreateDto
        {
            Name = entity.Name
        };
    }

    public RoleReadDto MapToRoleReadDto(Role entity)
    {
        return new RoleReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedDate = entity.CreatedDate
        };
    }

    public RoleUpdateDto MapToRoleUpdateDto(Role entity)
    {
        return new RoleUpdateDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public Role MapToRoleFromUpdateDto(RoleUpdateDto dto)
    {
        return new Role
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}
