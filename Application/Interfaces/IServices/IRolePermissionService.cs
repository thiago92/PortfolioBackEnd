using Application.Dtos;
using Infrastructure.FiltersModel;

namespace Application.Interfaces.IServices;

public interface IRolePermissionService
{
    RolePermissionReadDto GetById(FilterRolePermissionById filterRolePermissionById);
    FilterReturn<RolePermissionReadDto> GetFilter(FilterRolePermissionTable filter);
    RolePermissionUpdateDto Add(RolePermissionCreateDto rolePermissionCreateDto);
    void Update(RolePermissionUpdateDto rolePermissionUpdateDto);
    void Delete(Guid id);
}
