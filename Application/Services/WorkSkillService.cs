using Application.Dtos;
using Application.Interfaces.IFactories;
using Application.Interfaces.IServices;
using Application.Specification;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.ICache.IServices;

namespace Application.Services;

public class WorkSkillService : IWorkSkillService
{
    private readonly IWorkSkillRepository _workSkillRepository;
    private readonly IWorkSkillFactory _workSkillFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;
    private readonly WorkSkillSpecification _workSkillSpecification;

    public WorkSkillService(
        IWorkSkillRepository workSkillRepository,
        IWorkSkillFactory workSkillFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _workSkillRepository = workSkillRepository;
        _workSkillFactory = workSkillFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
        _workSkillSpecification = new WorkSkillSpecification(notificationContext);
    }

    public WorkSkillReadDto GetById(FilterWorkSkillById filterWorkSkillById)
    {
        string cacheKey = $"WorkSkill:Id:{filterWorkSkillById.Id}";
        var cached = _cacheService.Get<WorkSkillReadDto>(cacheKey);
        if (cached != null) return cached;

        var workSkill = _workSkillRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = filterWorkSkillById.Id,
            Key = "Equal",
            Includes = filterWorkSkillById.Includes
        });

        var dto = _workSkillFactory.MapToWorkSkillReadDto(workSkill);
        _cacheService.Set(cacheKey, dto, TimeSpan.FromMinutes(10));
        return dto;
    }

    public FilterReturn<WorkSkillReadDto> GetFilter(FilterWorkSkillTable filter)
    {
        string cacheKey = $"WorkSkill:Filter:{filter.GetHashCode()}";
        var cached = _cacheService.Get<FilterReturn<WorkSkillReadDto>>(cacheKey);
        if (cached != null) return cached;

        var filterResult = _workSkillRepository.GetFilter(filter);
        var result = new FilterReturn<WorkSkillReadDto>
        {
            TotalRegister = filterResult.TotalRegister,
            TotalRegisterFilter = filterResult.TotalRegisterFilter,
            TotalPages = filterResult.TotalPages,
            ItensList = filterResult.ItensList.Select(_workSkillFactory.MapToWorkSkillReadDto)
        };

        _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }

    public WorkSkillUpdateDto Add(WorkSkillCreateDto workSkillCreateDto)
    {
        WorkSkillUpdateDto workSkillUpdateDto = null;

        if (!_workSkillSpecification.IsSatisfiedBy(workSkillCreateDto)) return workSkillUpdateDto;
        if (!_workSkillRepository.ValidateInput(workSkillCreateDto, false)) return workSkillUpdateDto;

        var workSkill = _workSkillFactory.MapToWorkSkill(workSkillCreateDto);
        workSkillUpdateDto = _workSkillFactory.MapToWorkSkillUpdateDto(_workSkillRepository.Add(workSkill));

        return workSkillUpdateDto;
    }

    public void Update(WorkSkillUpdateDto workSkillUpdateDto)
    {
        if (!_workSkillSpecification.IsSatisfiedBy(workSkillUpdateDto)) return;

        var existingWorkSkill = _workSkillRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = workSkillUpdateDto.Id,
            Key = "Equal"
        });

        if (!_workSkillRepository.ValidateInput(workSkillUpdateDto, true, existingWorkSkill)) return;

        var workSkill = _workSkillFactory.MapToWorkSkillFromUpdateDto(workSkillUpdateDto);
        _workSkillRepository.Update(workSkill);

        _cacheService.Remove($"WorkSkill:Id:{workSkillUpdateDto.Id}");
        _cacheService.RemoveByPrefix("WorkSkill:Filter:");
    }

    public void Delete(Guid id)
    {
        var existingWorkSkill = _workSkillRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = id,
            Key = "Equal"
        });

        if (existingWorkSkill is null) return;

        _workSkillRepository.Delete(existingWorkSkill);

        _cacheService.Remove($"WorkSkill:Id:{id}");
        _cacheService.RemoveByPrefix("WorkSkill:Filter:");
    }
}
