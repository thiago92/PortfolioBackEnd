using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IFactories;

public interface IPermissionFactory
{
    // Criação de objetos vazios
    Permission CreatePermission();
    PermissionCreateDto CreatePermissionCreateDto();
    PermissionReadDto CreatePermissionReadDto();
    PermissionUpdateDto CreatePermissionUpdateDto();

    // Mapeamento DTO → Entidade
    Permission MapToPermission(PermissionCreateDto dto);
    Permission MapToPermissionFromUpdateDto(PermissionUpdateDto dto);

    // Mapeamento Entidade → DTO
    PermissionCreateDto MapToPermissionCreateDto(Permission entity);
    PermissionReadDto MapToPermissionReadDto(Permission entity);
    PermissionUpdateDto MapToPermissionUpdateDto(Permission entity);
}
