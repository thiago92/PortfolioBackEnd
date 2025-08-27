using Application.Dtos;
using Application.Interfaces.IFactories;
using Domain.Entities;

namespace Application.Factories;

public class WorkFactory : IWorkFactory
{
    // Criar uma nova entidade Work com valores padrão
    public Work CreateWork()
    {
        return new Work
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Url = string.Empty,
            Image = string.Empty,
            AltImage = string.Empty,
            IsFreelance = false,
            CreatedDate = DateTime.UtcNow,
            WorkSkills = new List<WorkSkill>()
        };
    }

    // Criar um WorkCreateDto vazio
    public WorkCreateDto CreateWorkCreateDto()
    {
        return new WorkCreateDto
        {
            Name = string.Empty,
            Url = string.Empty,
            Image = string.Empty,
            AltImage = string.Empty,
            IsFreelance = false
        };
    }

    // Criar um WorkReadDto vazio
    public WorkReadDto CreateWorkReadDto()
    {
        return new WorkReadDto
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Url = string.Empty,
            Image = string.Empty,
            AltImage = string.Empty,
            IsFreelance = false,
            CreatedDate = DateTime.UtcNow
        };
    }

    // Criar um WorkUpdateDto vazio
    public WorkUpdateDto CreateWorkUpdateDto()
    {
        return new WorkUpdateDto
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Url = string.Empty,
            Image = string.Empty,
            AltImage = string.Empty,
            IsFreelance = false
        };
    }

    // Mapeamentos

    public Work MapToWork(WorkCreateDto dto)
    {
        return new Work
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Url = dto.Url,
            Image = dto.Image,
            AltImage = dto.AltImage,
            IsFreelance = dto.IsFreelance,
            CreatedDate = DateTime.UtcNow,
            WorkSkills = new List<WorkSkill>()
        };
    }

    public WorkCreateDto MapToWorkCreateDto(Work entity)
    {
        return new WorkCreateDto
        {
            Name = entity.Name,
            Url = entity.Url,
            Image = entity.Image,
            AltImage = entity.AltImage,
            IsFreelance = entity.IsFreelance
        };
    }

    public WorkReadDto MapToWorkReadDto(Work entity)
    {
        return new WorkReadDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Url = entity.Url,
            Image = entity.Image,
            AltImage = entity.AltImage,
            IsFreelance = entity.IsFreelance,
            CreatedDate = entity.CreatedDate
        };
    }

    public WorkUpdateDto MapToWorkUpdateDto(Work entity)
    {
        return new WorkUpdateDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Url = entity.Url,
            Image = entity.Image,
            AltImage = entity.AltImage,
            IsFreelance = entity.IsFreelance
        };
    }

    public Work MapToWorkFromUpdateDto(WorkUpdateDto dto)
    {
        return new Work
        {
            Id = dto.Id,
            Name = dto.Name,
            Url = dto.Url,
            Image = dto.Image,
            AltImage = dto.AltImage,
            IsFreelance = dto.IsFreelance,
            WorkSkills = new List<WorkSkill>()
        };
    }
}
