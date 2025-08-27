using Application.Dtos;
using Application.Interfaces.IFactories;
using Domain.Entities;

namespace Application.Factories;

public class WorkSkillFactory : IWorkSkillFactory
{
    // Criar uma nova entidade WorkSkill com valores padrão
    public WorkSkill CreateWorkSkill()
    {
        return new WorkSkill
        {
            Id = Guid.NewGuid(),
            WorkId = Guid.Empty,
            SkillId = Guid.Empty
        };
    }

    // Criar um WorkSkillCreateDto vazio
    public WorkSkillCreateDto CreateWorkSkillCreateDto()
    {
        return new WorkSkillCreateDto
        {
            WorkId = Guid.Empty,
            SkillId = Guid.Empty
        };
    }

    // Criar um WorkSkillReadDto vazio
    public WorkSkillReadDto CreateWorkSkillReadDto()
    {
        return new WorkSkillReadDto
        {
            Id = Guid.NewGuid(),
            WorkId = Guid.Empty,
            SkillId = Guid.Empty
        };
    }

    // Criar um WorkSkillUpdateDto vazio
    public WorkSkillUpdateDto CreateWorkSkillUpdateDto()
    {
        return new WorkSkillUpdateDto
        {
            Id = Guid.NewGuid(),
            WorkId = Guid.Empty,
            SkillId = Guid.Empty
        };
    }

    // Mapeamentos

    public WorkSkill MapToWorkSkill(WorkSkillCreateDto dto)
    {
        return new WorkSkill
        {
            Id = Guid.NewGuid(),
            WorkId = dto.WorkId,
            SkillId = dto.SkillId
        };
    }

    public WorkSkillCreateDto MapToWorkSkillCreateDto(WorkSkill entity)
    {
        return new WorkSkillCreateDto
        {
            WorkId = entity.WorkId,
            SkillId = entity.SkillId
        };
    }

    public WorkSkillReadDto MapToWorkSkillReadDto(WorkSkill entity)
    {
        return new WorkSkillReadDto
        {
            Id = entity.Id,
            WorkId = entity.WorkId,
            SkillId = entity.SkillId
        };
    }

    public WorkSkillUpdateDto MapToWorkSkillUpdateDto(WorkSkill entity)
    {
        return new WorkSkillUpdateDto
        {
            Id = entity.Id,
            WorkId = entity.WorkId,
            SkillId = entity.SkillId
        };
    }

    public WorkSkill MapToWorkSkillFromUpdateDto(WorkSkillUpdateDto dto)
    {
        return new WorkSkill
        {
            Id = dto.Id,
            WorkId = dto.WorkId,
            SkillId = dto.SkillId
        };
    }
}
