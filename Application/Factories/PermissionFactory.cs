using Application.Dtos;
using Application.Interfaces.IFactories;
using Domain.Entities;

namespace Application.Factories;

public class PermissionFactory : IPermissionFactory
{
    // Criar uma nova entidade Permission com valores padrão
    public Permission CreatePermission()
    {
        return new Permission
        {
            Id = Guid.NewGuid(),
            Resource = string.Empty,
            CanView = false,
            CanCreate = false,
            CanEdit = false,
            CanDelete = false
        };
    }

    // Criar um PermissionCreateDto vazio
    public PermissionCreateDto CreatePermissionCreateDto()
    {
        return new PermissionCreateDto
        {
            Resource = string.Empty,
            CanView = false,
            CanCreate = false,
            CanEdit = false,
            CanDelete = false
        };
    }

    // Criar um PermissionReadDto vazio
    public PermissionReadDto CreatePermissionReadDto()
    {
        return new PermissionReadDto
        {
            Id = Guid.NewGuid(),
            Resource = string.Empty,
            CanView = false,
            CanCreate = false,
            CanEdit = false,
            CanDelete = false
        };
    }

    // Criar um PermissionUpdateDto vazio
    public PermissionUpdateDto CreatePermissionUpdateDto()
    {
        return new PermissionUpdateDto
        {
            Id = Guid.NewGuid(),
            Resource = string.Empty,
            CanView = false,
            CanCreate = false,
            CanEdit = false,
            CanDelete = false
        };
    }

    // Mapeamentos

    public Permission MapToPermission(PermissionCreateDto dto)
    {
        return new Permission
        {
            Id = Guid.NewGuid(),
            Resource = dto.Resource,
            CanView = dto.CanView,
            CanCreate = dto.CanCreate,
            CanEdit = dto.CanEdit,
            CanDelete = dto.CanDelete
        };
    }

    public PermissionCreateDto MapToPermissionCreateDto(Permission entity)
    {
        return new PermissionCreateDto
        {
            Resource = entity.Resource,
            CanView = entity.CanView,
            CanCreate = entity.CanCreate,
            CanEdit = entity.CanEdit,
            CanDelete = entity.CanDelete
        };
    }

    public PermissionReadDto MapToPermissionReadDto(Permission entity)
    {
        return new PermissionReadDto
        {
            Id = entity.Id,
            Resource = entity.Resource,
            CanView = entity.CanView,
            CanCreate = entity.CanCreate,
            CanEdit = entity.CanEdit,
            CanDelete = entity.CanDelete
        };
    }

    public PermissionUpdateDto MapToPermissionUpdateDto(Permission entity)
    {
        return new PermissionUpdateDto
        {
            Id = entity.Id,
            Resource = entity.Resource,
            CanView = entity.CanView,
            CanCreate = entity.CanCreate,
            CanEdit = entity.CanEdit,
            CanDelete = entity.CanDelete
        };
    }

    public Permission MapToPermissionFromUpdateDto(PermissionUpdateDto dto)
    {
        return new Permission
        {
            Id = dto.Id,
            Resource = dto.Resource,
            CanView = dto.CanView,
            CanCreate = dto.CanCreate,
            CanEdit = dto.CanEdit,
            CanDelete = dto.CanDelete
        };
    }
}
