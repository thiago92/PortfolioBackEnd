using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IFactories;

public interface IUserFactory
{
    // Criação de objetos vazios
    User CreateUser();
    UserCreateDto CreateUserCreateDto();
    UserReadDto CreateUserReadDto();
    UserUpdateDto CreateUserUpdateDto();

    // Mapeamento DTO → Entidade
    User MapToUser(UserCreateDto dto);
    User MapToUserFromUpdateDto(UserUpdateDto dto);

    // Mapeamento Entidade → DTO
    UserCreateDto MapToUserCreateDto(User entity);
    UserReadDto MapToUserReadDto(User entity);
    UserUpdateDto MapToUserUpdateDto(User entity);
}
