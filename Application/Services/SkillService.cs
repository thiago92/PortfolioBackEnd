using Application.Dtos;
using Application.Interfaces.IFactories;
using Application.Interfaces.IServices;
using Application.Specification;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.ICache.IServices;

namespace Application.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly ISkillFactory _skillFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;
    private readonly SkillSpecification _skillSpecification;

    public SkillService(
        ISkillRepository skillRepository,
        ISkillFactory skillFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _skillRepository = skillRepository;
        _skillFactory = skillFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
        _skillSpecification = new SkillSpecification(notificationContext);
    }

    public SkillReadDto GetById(FilterSkillById filterSkillById)
    {
        string cacheKey = $"Skill:Id:{filterSkillById.Id}";
        var cached = _cacheService.Get<SkillReadDto>(cacheKey);
        if (cached != null) return cached;

        var skill = _skillRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = filterSkillById.Id,
            Key = "Equal",
            Includes = filterSkillById.Includes
        });

        var dto = _skillFactory.MapToSkillReadDto(skill);
        _cacheService.Set(cacheKey, dto, TimeSpan.FromMinutes(10));
        return dto;
    }

    public FilterReturn<SkillReadDto> GetFilter(FilterSkillTable filter)
    {
        string cacheKey = $"Skill:Filter:{filter.GetHashCode()}";
        var cached = _cacheService.Get<FilterReturn<SkillReadDto>>(cacheKey);
        if (cached != null) return cached;

        var filterResult = _skillRepository.GetFilter(filter);
        var result = new FilterReturn<SkillReadDto>
        {
            TotalRegister = filterResult.TotalRegister,
            TotalRegisterFilter = filterResult.TotalRegisterFilter,
            TotalPages = filterResult.TotalPages,
            ItensList = filterResult.ItensList.Select(_skillFactory.MapToSkillReadDto)
        };

        _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }

    public SkillUpdateDto Add(SkillCreateDto skillCreateDto)
    {
        SkillUpdateDto skillUpdateDto = null;
        if (!_skillSpecification.IsSatisfiedBy(skillCreateDto)) return skillUpdateDto;
        if (!_skillRepository.ValidateInput(skillCreateDto, false)) return skillUpdateDto;

        var skill = _skillFactory.MapToSkill(skillCreateDto);
        skillUpdateDto = _skillFactory.MapToSkillUpdateDto(_skillRepository.Add(skill));
        return skillUpdateDto;
    }

    public void Update(SkillUpdateDto skillUpdateDto)
    {
        if (!_skillSpecification.IsSatisfiedBy(skillUpdateDto)) return;

        var existingSkill = _skillRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = skillUpdateDto.Id,
            Key = "Equal"
        });

        if (!_skillRepository.ValidateInput(skillUpdateDto, true, existingSkill)) return;

        var skill = _skillFactory.MapToSkillFromUpdateDto(skillUpdateDto);
        _skillRepository.Update(skill);

        _cacheService.Remove($"Skill:Id:{skillUpdateDto.Id}");
        _cacheService.RemoveByPrefix("Skill:Filter:");
    }

    public void Delete(Guid id)
    {
        var existingSkill = _skillRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = id,
            Key = "Equal"
        });

        if (existingSkill is null) return;

        _skillRepository.Delete(existingSkill);

        _cacheService.Remove($"Skill:Id:{id}");
        _cacheService.RemoveByPrefix("Skill:Filter:");
    }
}
