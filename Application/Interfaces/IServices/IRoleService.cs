using Application.Dtos;
using Infrastructure.FiltersModel;

namespace Application.Interfaces.IServices;

public interface IRoleService
{
    RoleReadDto GetById(FilterRoleById filterRoleById);
    FilterReturn<RoleReadDto> GetFilter(FilterRoleTable filter);
    RoleUpdateDto Add(RoleCreateDto roleCreateDto);
    void Update(RoleUpdateDto roleUpdateDto);
    void Delete(Guid id);
}
