using Application.Dtos;
using Application.Interfaces.IFactories;
using Domain.Entities;

namespace Application.Factories;

public class RolePermissionFactory : IRolePermissionFactory
{
    // Criar uma nova entidade RolePermission com valores padrão
    public RolePermission CreateRolePermission()
    {
        return new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = Guid.Empty,
            PermissionId = Guid.Empty
        };
    }

    // Criar um RolePermissionCreateDto vazio
    public RolePermissionCreateDto CreateRolePermissionCreateDto()
    {
        return new RolePermissionCreateDto
        {
            RoleId = Guid.Empty,
            PermissionId = Guid.Empty
        };
    }

    // Criar um RolePermissionReadDto vazio
    public RolePermissionReadDto CreateRolePermissionReadDto()
    {
        return new RolePermissionReadDto
        {
            Id = Guid.NewGuid(),
            RoleId = Guid.Empty,
            PermissionId = Guid.Empty
        };
    }

    // Criar um RolePermissionUpdateDto vazio
    public RolePermissionUpdateDto CreateRolePermissionUpdateDto()
    {
        return new RolePermissionUpdateDto
        {
            Id = Guid.NewGuid(),
            RoleId = Guid.Empty,
            PermissionId = Guid.Empty
        };
    }

    // Mapeamentos

    public RolePermission MapToRolePermission(RolePermissionCreateDto dto)
    {
        return new RolePermission
        {
            Id = Guid.NewGuid(),
            RoleId = dto.RoleId,
            PermissionId = dto.PermissionId
        };
    }

    public RolePermissionCreateDto MapToRolePermissionCreateDto(RolePermission entity)
    {
        return new RolePermissionCreateDto
        {
            RoleId = entity.RoleId,
            PermissionId = entity.PermissionId
        };
    }

    public RolePermissionReadDto MapToRolePermissionReadDto(RolePermission entity)
    {
        return new RolePermissionReadDto
        {
            Id = entity.Id,
            RoleId = entity.RoleId,
            PermissionId = entity.PermissionId
        };
    }

    public RolePermissionUpdateDto MapToRolePermissionUpdateDto(RolePermission entity)
    {
        return new RolePermissionUpdateDto
        {
            Id = entity.Id,
            RoleId = entity.RoleId,
            PermissionId = entity.PermissionId
        };
    }

    public RolePermission MapToRolePermissionFromUpdateDto(RolePermissionUpdateDto dto)
    {
        return new RolePermission
        {
            Id = dto.Id,
            RoleId = dto.RoleId,
            PermissionId = dto.PermissionId
        };
    }
}
