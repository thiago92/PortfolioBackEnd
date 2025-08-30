using Application.Dtos;
using Application.Interfaces.IFactories;
using Application.Interfaces.IServices;
using Application.Specification;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.ICache.IServices;

namespace Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRolePermissionFactory _rolePermissionFactory;
        private readonly ICacheService _cacheService;
        private readonly NotificationContext _notificationContext;
        private readonly RolePermissionSpecification _rolePermissionSpecification;

        public RolePermissionService(
            IRolePermissionRepository rolePermissionRepository,
            IRolePermissionFactory rolePermissionFactory,
            ICacheService cacheService,
            NotificationContext notifierContext)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _rolePermissionFactory = rolePermissionFactory;
            _cacheService = cacheService;
            _notificationContext = notifierContext;
            _rolePermissionSpecification = new RolePermissionSpecification(_notificationContext);
        }

        public RolePermissionReadDto GetById(FilterRolePermissionById filterRolePermissionById)
        {
            string cacheKey = $"RolePermission:Id:{filterRolePermissionById.Id}";
            var cached = _cacheService.Get<RolePermissionReadDto>(cacheKey);

            if (cached != null) return cached;

            var rolePermission = _rolePermissionRepository.GetByElement(new FilterByItem
            {
                Field = "Id",
                Value = filterRolePermissionById.Id,
                Key = "Equal",
                Includes = filterRolePermissionById.Includes
            });

            var dto = _rolePermissionFactory.MapToRolePermissionReadDto(rolePermission);
            _cacheService.Set(cacheKey, dto, TimeSpan.FromMinutes(10));
            return dto;
        }

        public FilterReturn<RolePermissionReadDto> GetFilter(FilterRolePermissionTable filter)
        {
            string cacheKey = $"RolePermission:Filter:{filter.GetHashCode()}";
            var cached = _cacheService.Get<FilterReturn<RolePermissionReadDto>>(cacheKey);

            if (cached != null) return cached;

            var filterResult = _rolePermissionRepository.GetFilter(filter);
            var result = new FilterReturn<RolePermissionReadDto>
            {
                TotalRegister = filterResult.TotalRegister,
                TotalRegisterFilter = filterResult.TotalRegisterFilter,
                TotalPages = filterResult.TotalPages,
                ItensList = filterResult.ItensList.Select(_rolePermissionFactory.MapToRolePermissionReadDto)
            };

            _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(10));
            return result;
        }

        public RolePermissionUpdateDto Add(RolePermissionCreateDto rolePermissionCreateDto)
        {
            RolePermissionUpdateDto rolePermissionUpdateDto = null;

            if (!_rolePermissionSpecification.IsSatisfiedBy(rolePermissionCreateDto)) return rolePermissionUpdateDto;

            if (!_rolePermissionRepository.ValidateInput(rolePermissionCreateDto, false)) return rolePermissionUpdateDto;

            var rolePermission = _rolePermissionFactory.MapToRolePermission(rolePermissionCreateDto);

            var addedRolePermission = _rolePermissionRepository.Add(rolePermission);

            rolePermissionUpdateDto = _rolePermissionFactory.MapToRolePermissionUpdateDto(addedRolePermission);

            return rolePermissionUpdateDto;
        }

        public void Update(RolePermissionUpdateDto rolePermissionUpdateDto)
        {
            if (!_rolePermissionSpecification.IsSatisfiedBy(rolePermissionUpdateDto)) return;

            var existingRolePermission = _rolePermissionRepository.GetByElement(new FilterByItem
            {
                Field = "Id",
                Value = rolePermissionUpdateDto.Id,
                Key = "Equal"
            });

            if (!_rolePermissionRepository.ValidateInput(rolePermissionUpdateDto, false, existingRolePermission)) return;

            var rolePermission = _rolePermissionFactory.MapToRolePermissionFromUpdateDto(rolePermissionUpdateDto);

            _rolePermissionRepository.Update(rolePermission);

            string cacheKey = $"RolePermission:Id:{rolePermissionUpdateDto.Id}";
            _cacheService.Remove(cacheKey);
            _cacheService.RemoveByPrefix("RolePermission:Filter:");
        }

        public void Delete(Guid id)
        {
            var existingRolePermission = _rolePermissionRepository.GetByElement(new FilterByItem
            {
                Field = "Id",
                Value = id,
                Key = "Equal"
            });

            if (existingRolePermission is null) return;

            _rolePermissionRepository.Delete(existingRolePermission);

            string cacheKey = $"RolePermission:Id:{id}";
            _cacheService.Remove(cacheKey);
            _cacheService.RemoveByPrefix("RolePermission:Filter:");
        }
    }
}
