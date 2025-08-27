using Application.Dtos;
using Application.Interfaces.IFactories;
using Domain.Entities;

namespace Application.Factories;

public class SkillFactory : ISkillFactory
{
    // Criar uma nova entidade Skill com valores padrão
    public Skill CreateSkill()
    {
        return new Skill
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Value = 0,
            CreatedDate = DateTime.UtcNow,
            WorkSkills = new List<WorkSkill>()
        };
    }

    // Criar um SkillCreateDto vazio
    public SkillCreateDto CreateSkillCreateDto()
    {
        return new SkillCreateDto
        {
            Name = string.Empty,
            Value = 0
        };
    }

    // Criar um SkillReadDto vazio
    public SkillReadDto CreateSkillReadDto()
    {
        return new SkillReadDto
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Value = 0
        };
    }

    // Criar um SkillUpdateDto vazio
    public SkillUpdateDto CreateSkillUpdateDto()
    {
        return new SkillUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Value = 0
        };
    }

    // Mapeamentos

    public Skill MapToSkill(SkillCreateDto dto)
    {
        return new Skill
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Value = dto.Value,
            CreatedDate = DateTime.UtcNow,
            WorkSkills = new List<WorkSkill>()
        };
    }

    public SkillCreateDto MapToSkillCreateDto(Skill entity)
    {
        return new SkillCreateDto
        {
            Name = entity.Name,
            Value = entity.Value
        };
    }

    public SkillReadDto MapToSkillReadDto(Skill entity)
    {
        return new SkillReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Value = entity.Value
        };
    }

    public SkillUpdateDto MapToSkillUpdateDto(Skill entity)
    {
        return new SkillUpdateDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Value = entity.Value
        };
    }

    public Skill MapToSkillFromUpdateDto(SkillUpdateDto dto)
    {
        return new Skill
        {
            Id = dto.Id,
            Name = dto.Name,
            Value = dto.Value
        };
    }
}
