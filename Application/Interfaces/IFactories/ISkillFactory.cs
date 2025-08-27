using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IFactories;

public interface ISkillFactory
{
    // Criação de objetos vazios
    Skill CreateSkill();
    SkillCreateDto CreateSkillCreateDto();
    SkillReadDto CreateSkillReadDto();
    SkillUpdateDto CreateSkillUpdateDto();

    // Mapeamento DTO → Entidade
    Skill MapToSkill(SkillCreateDto dto);
    Skill MapToSkillFromUpdateDto(SkillUpdateDto dto);

    // Mapeamento Entidade → DTO
    SkillCreateDto MapToSkillCreateDto(Skill entity);
    SkillReadDto MapToSkillReadDto(Skill entity);
    SkillUpdateDto MapToSkillUpdateDto(Skill entity);
}
