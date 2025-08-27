using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IFactories;

public interface IRoleFactory
{
    // Criação de objetos vazios
    Role CreateRole();
    RoleCreateDto CreateRoleCreateDto();
    RoleReadDto CreateRoleReadDto();
    RoleUpdateDto CreateRoleUpdateDto();

    // Mapeamento DTO → Entidade
    Role MapToRole(RoleCreateDto dto);
    Role MapToRoleFromUpdateDto(RoleUpdateDto dto);

    // Mapeamento Entidade → DTO
    RoleCreateDto MapToRoleCreateDto(Role entity);
    RoleReadDto MapToRoleReadDto(Role entity);
    RoleUpdateDto MapToRoleUpdateDto(Role entity);
}
