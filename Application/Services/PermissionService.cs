using Application.Dtos;
using Application.Interfaces.IFactories;
using Application.Interfaces.IServices;
using Application.Specification;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.ICache.IServices;

namespace Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionFactory _permissionFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;
    private readonly PermissionSpecification _permissionSpecification;

    public PermissionService(
        IPermissionRepository permissionRepository,
        IPermissionFactory permissionFactory,
        ICacheService cacheService,
        NotificationContext notifierContext)
    {
        _permissionRepository = permissionRepository;
        _permissionFactory = permissionFactory;
        _cacheService = cacheService;
        _notificationContext = notifierContext;
        _permissionSpecification = new PermissionSpecification(notifierContext);
    }

    public PermissionReadDto GetById(FilterPermissionById filterPermissionById)
    {
        string cacheKey = $"Permission:Id:{filterPermissionById.Id}";
        var cached = _cacheService.Get<PermissionReadDto>(cacheKey);

        if (cached != null) return cached;

        var permission = _permissionRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = filterPermissionById.Id,
            Key = "Equal",
            Includes = filterPermissionById.Includes
        });

        var dto = _permissionFactory.MapToPermissionReadDto(permission);
        _cacheService.Set(cacheKey, dto, TimeSpan.FromMinutes(10));
        return dto;
    }

    public FilterReturn<PermissionReadDto> GetFilter(FilterPermissionTable filter)
    {
        string cacheKey = $"Permission:Filter:{filter.GetHashCode()}";
        var cached = _cacheService.Get<FilterReturn<PermissionReadDto>>(cacheKey);

        if (cached != null) return cached;

        var filterResult = _permissionRepository.GetFilter(filter);
        var result = new FilterReturn<PermissionReadDto>
        {
            TotalRegister = filterResult.TotalRegister,
            TotalRegisterFilter = filterResult.TotalRegisterFilter,
            TotalPages = filterResult.TotalPages,
            ItensList = filterResult.ItensList.Select(_permissionFactory.MapToPermissionReadDto)
        };
        _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }

    public PermissionUpdateDto Add(PermissionCreateDto permissionCreateDto)
    {
        PermissionUpdateDto permissionUpdateDto = null;

        if (!_permissionSpecification.IsSatisfiedBy(permissionCreateDto)) return permissionUpdateDto;

        if (!_permissionRepository.ValidateInput(permissionCreateDto, false)) return permissionUpdateDto;

        var permission = _permissionFactory.MapToPermission(permissionCreateDto);

        var addedPermission = _permissionRepository.Add(permission);

        permissionUpdateDto = _permissionFactory.MapToPermissionUpdateDto(addedPermission);

        return permissionUpdateDto;
    }

    public void Update(PermissionUpdateDto permissionUpdateDto)
    {
        if (!_permissionSpecification.IsSatisfiedBy(permissionUpdateDto)) return;

        var existingPermission = _permissionRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = permissionUpdateDto.Id,
            Key = "Equal"
        });

        if (!_permissionRepository.ValidateInput(permissionUpdateDto, false, existingPermission)) return;

        var permission = _permissionFactory.MapToPermissionFromUpdateDto(permissionUpdateDto);

        _permissionRepository.Update(permission);

        string cacheKey = $"Permission:Id:{permissionUpdateDto.Id}";
        _cacheService.Remove(cacheKey);
        _cacheService.RemoveByPrefix("Permission:Filter:");
    }

    public void Delete(Guid id)
    {
        var existingPermission = _permissionRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = id,
            Key = "Equal"
        });

        if (existingPermission is null) return;

        _permissionRepository.Delete(existingPermission);

        string cacheKey = $"Permission:Id:{id}";
        _cacheService.Remove(cacheKey);
        _cacheService.RemoveByPrefix("Permission:Filter:");
    }
}
