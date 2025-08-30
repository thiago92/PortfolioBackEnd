using Application.Dtos;
using Application.Interfaces.IFactories;
using Application.Interfaces.IServices;
using Application.Specification;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.ICache.IServices;

namespace Application.Services;

public class WorkService : IWorkService
{
    private readonly IWorkRepository _workRepository;
    private readonly IWorkFactory _workFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;
    private readonly WorkSpecification _workSpecification;

    public WorkService(
        IWorkRepository workRepository,
        IWorkFactory workFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _workRepository = workRepository;
        _workFactory = workFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
        _workSpecification = new WorkSpecification(notificationContext);
    }

    public WorkReadDto GetById(FilterWorkById filterWorkById)
    {
        string cacheKey = $"Work:Id:{filterWorkById.Id}";
        var cached = _cacheService.Get<WorkReadDto>(cacheKey);
        if (cached != null) return cached;

        var work = _workRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = filterWorkById.Id,
            Key = "Equal",
            Includes = filterWorkById.Includes
        });

        var dto = _workFactory.MapToWorkReadDto(work);
        _cacheService.Set(cacheKey, dto, TimeSpan.FromMinutes(10));
        return dto;
    }

    public FilterReturn<WorkReadDto> GetFilter(FilterWorkTable filter)
    {
        string cacheKey = $"Work:Filter:{filter.GetHashCode()}";
        var cached = _cacheService.Get<FilterReturn<WorkReadDto>>(cacheKey);
        if (cached != null) return cached;

        var filterResult = _workRepository.GetFilter(filter);
        var result = new FilterReturn<WorkReadDto>
        {
            TotalRegister = filterResult.TotalRegister,
            TotalRegisterFilter = filterResult.TotalRegisterFilter,
            TotalPages = filterResult.TotalPages,
            ItensList = filterResult.ItensList.Select(_workFactory.MapToWorkReadDto)
        };

        _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }

    public WorkUpdateDto Add(WorkCreateDto workCreateDto)
    {
        WorkUpdateDto workUpdateDto = null;
        if (!_workSpecification.IsSatisfiedBy(workCreateDto)) return workUpdateDto;
        if (!_workRepository.ValidateInput(workCreateDto, false)) return workUpdateDto;

        var work = _workFactory.MapToWork(workCreateDto);
        workUpdateDto = _workFactory.MapToWorkUpdateDto(_workRepository.Add(work));
        return workUpdateDto;
    }

    public void Update(WorkUpdateDto workUpdateDto)
    {
        if (!_workSpecification.IsSatisfiedBy(workUpdateDto)) return;

        var existingWork = _workRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = workUpdateDto.Id,
            Key = "Equal"
        });

        if (!_workRepository.ValidateInput(workUpdateDto, true, existingWork)) return;

        var work = _workFactory.MapToWorkFromUpdateDto(workUpdateDto);
        _workRepository.Update(work);

        _cacheService.Remove($"Work:Id:{workUpdateDto.Id}");
        _cacheService.RemoveByPrefix("Work:Filter:");
    }

    public void Delete(Guid id)
    {
        var existingWork = _workRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = id,
            Key = "Equal"
        });

        if (existingWork is null) return;

        _workRepository.Delete(existingWork);

        _cacheService.Remove($"Work:Id:{id}");
        _cacheService.RemoveByPrefix("Work:Filter:");
    }
}
