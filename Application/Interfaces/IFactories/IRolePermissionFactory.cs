using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IFactories;

public interface IRolePermissionFactory
{
    // Criação de objetos vazios
    RolePermission CreateRolePermission();
    RolePermissionCreateDto CreateRolePermissionCreateDto();
    RolePermissionReadDto CreateRolePermissionReadDto();
    RolePermissionUpdateDto CreateRolePermissionUpdateDto();

    // Mapeamento DTO → Entidade
    RolePermission MapToRolePermission(RolePermissionCreateDto dto);
    RolePermission MapToRolePermissionFromUpdateDto(RolePermissionUpdateDto dto);

    // Mapeamento Entidade → DTO
    RolePermissionCreateDto MapToRolePermissionCreateDto(RolePermission entity);
    RolePermissionReadDto MapToRolePermissionReadDto(RolePermission entity);
    RolePermissionUpdateDto MapToRolePermissionUpdateDto(RolePermission entity);
}
