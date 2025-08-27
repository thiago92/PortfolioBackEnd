using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IFactories;

public interface IWorkSkillFactory
{
    // Criação de objetos vazios
    WorkSkill CreateWorkSkill();
    WorkSkillCreateDto CreateWorkSkillCreateDto();
    WorkSkillReadDto CreateWorkSkillReadDto();
    WorkSkillUpdateDto CreateWorkSkillUpdateDto();

    // Mapeamento DTO → Entidade
    WorkSkill MapToWorkSkill(WorkSkillCreateDto dto);
    WorkSkill MapToWorkSkillFromUpdateDto(WorkSkillUpdateDto dto);

    // Mapeamento Entidade → DTO
    WorkSkillCreateDto MapToWorkSkillCreateDto(WorkSkill entity);
    WorkSkillReadDto MapToWorkSkillReadDto(WorkSkill entity);
    WorkSkillUpdateDto MapToWorkSkillUpdateDto(WorkSkill entity);
}
