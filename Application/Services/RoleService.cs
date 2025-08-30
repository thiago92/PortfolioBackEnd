using Application.Dtos;
using Application.Interfaces.IFactories;
using Application.Interfaces.IServices;
using Application.Specification;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.ICache.IServices;

namespace Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleFactory _roleFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;
    private readonly RoleSpecification _roleSpecification;

    public RoleService(
        IRoleRepository roleRepository,
        IRoleFactory roleFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _roleRepository = roleRepository;
        _roleFactory = roleFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
        _roleSpecification = new RoleSpecification(notificationContext);
    }

    public RoleReadDto GetById(FilterRoleById filterRoleById)
    {
        string cacheKey = $"Role:Id:{filterRoleById.Id}";
        var cached = _cacheService.Get<RoleReadDto>(cacheKey);
        if (cached != null) return cached;

        var role = _roleRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = filterRoleById.Id,
            Key = "Equal",
            Includes = filterRoleById.Includes
        });

        var dto = _roleFactory.MapToRoleReadDto(role);
        _cacheService.Set(cacheKey, dto, TimeSpan.FromMinutes(10));
        return dto;
    }

    public FilterReturn<RoleReadDto> GetFilter(FilterRoleTable filter)
    {
        string cacheKey = $"Role:Filter:{filter.GetHashCode()}";
        var cached = _cacheService.Get<FilterReturn<RoleReadDto>>(cacheKey);
        if (cached != null) return cached;

        var filterResult = _roleRepository.GetFilter(filter);
        var result = new FilterReturn<RoleReadDto>
        {
            TotalRegister = filterResult.TotalRegister,
            TotalRegisterFilter = filterResult.TotalRegisterFilter,
            TotalPages = filterResult.TotalPages,
            ItensList = filterResult.ItensList.Select(_roleFactory.MapToRoleReadDto)
        };

        _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }

    public RoleUpdateDto Add(RoleCreateDto roleCreateDto)
    {
        RoleUpdateDto roleUpdateDto = null;
        if (!_roleSpecification.IsSatisfiedBy(roleCreateDto)) return roleUpdateDto;
        if (!_roleRepository.ValidateInput(roleCreateDto, false)) return roleUpdateDto;

        var role = _roleFactory.MapToRole(roleCreateDto);
        roleUpdateDto = _roleFactory.MapToRoleUpdateDto(_roleRepository.Add(role));
        return roleUpdateDto;
    }

    public void Update(RoleUpdateDto roleUpdateDto)
    {
        if (!_roleSpecification.IsSatisfiedBy(roleUpdateDto)) return;

        var existingRole = _roleRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = roleUpdateDto.Id,
            Key = "Equal"
        });

        if (!_roleRepository.ValidateInput(roleUpdateDto, true, existingRole)) return;

        var role = _roleFactory.MapToRoleFromUpdateDto(roleUpdateDto);
        _roleRepository.Update(role);

        _cacheService.Remove($"Role:Id:{roleUpdateDto.Id}");
        _cacheService.RemoveByPrefix("Role:Filter:");
    }

    public void Delete(Guid id)
    {
        var existingRole = _roleRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = id,
            Key = "Equal"
        });

        if (existingRole is null) return;

        _roleRepository.Delete(existingRole);

        _cacheService.Remove($"Role:Id:{id}");
        _cacheService.RemoveByPrefix("Role:Filter:");
    }
}
