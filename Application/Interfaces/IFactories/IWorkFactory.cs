using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IFactories;

public interface IWorkFactory
{
    // Criação de objetos vazios
    Work CreateWork();
    WorkCreateDto CreateWorkCreateDto();
    WorkReadDto CreateWorkReadDto();
    WorkUpdateDto CreateWorkUpdateDto();

    // Mapeamento DTO → Entidade
    Work MapToWork(WorkCreateDto dto);
    Work MapToWorkFromUpdateDto(WorkUpdateDto dto);

    // Mapeamento Entidade → DTO
    WorkCreateDto MapToWorkCreateDto(Work entity);
    WorkReadDto MapToWorkReadDto(Work entity);
    WorkUpdateDto MapToWorkUpdateDto(Work entity);
}
