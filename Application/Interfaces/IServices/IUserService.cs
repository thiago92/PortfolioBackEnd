using Application.Dtos;
using Infrastructure.FiltersModel;

namespace Application.Interfaces.IServices;

public interface IUserService
{
    UserReadDto GetById(FilterUserById filterUserById);
    FilterReturn<UserReadDto> GetFilter(FilterUserTable filter);
    UserUpdateDto Add(UserCreateDto userCreateDto);
    void Update(UserUpdateDto userUpdateDto);
    void Delete(Guid id);
}
