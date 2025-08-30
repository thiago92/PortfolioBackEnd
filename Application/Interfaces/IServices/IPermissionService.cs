using Application.Dtos;
using Infrastructure.FiltersModel;

namespace Application.Interfaces.IServices;

public interface IPermissionService
{
    PermissionReadDto GetById(FilterPermissionById filterPermissionById);
    FilterReturn<PermissionReadDto> GetFilter(FilterPermissionTable filter);
    PermissionUpdateDto Add(PermissionCreateDto permissionCreateDto);
    void Update(PermissionUpdateDto permissionUpdateDto);
    void Delete(Guid id);
}
