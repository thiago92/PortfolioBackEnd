using Application.Dtos;
using Application.Interfaces.IFactories;
using Application.Interfaces.IServices;
using Application.Specification;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.ICache.IServices;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserFactory _userFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;
    private readonly UserSpecification _userSpecification;

    public UserService(
        IUserRepository userRepository,
        IUserFactory userFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _userRepository = userRepository;
        _userFactory = userFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
        _userSpecification = new UserSpecification(notificationContext);
    }

    public UserReadDto GetById(FilterUserById filterUserById)
    {
        string cacheKey = $"User:Id:{filterUserById.Id}";
        var cached = _cacheService.Get<UserReadDto>(cacheKey);
        if (cached != null) return cached;

        var user = _userRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = filterUserById.Id,
            Key = "Equal",
            Includes = filterUserById.Includes
        });

        var dto = _userFactory.MapToUserReadDto(user);
        _cacheService.Set(cacheKey, dto, TimeSpan.FromMinutes(10));
        return dto;
    }

    public FilterReturn<UserReadDto> GetFilter(FilterUserTable filter)
    {
        string cacheKey = $"User:Filter:{filter.GetHashCode()}";
        var cached = _cacheService.Get<FilterReturn<UserReadDto>>(cacheKey);
        if (cached != null) return cached;

        var filterResult = _userRepository.GetFilter(filter);
        var result = new FilterReturn<UserReadDto>
        {
            TotalRegister = filterResult.TotalRegister,
            TotalRegisterFilter = filterResult.TotalRegisterFilter,
            TotalPages = filterResult.TotalPages,
            ItensList = filterResult.ItensList.Select(_userFactory.MapToUserReadDto)
        };

        _cacheService.Set(cacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }

    public UserUpdateDto Add(UserCreateDto userCreateDto)
    {
        UserUpdateDto userUpdateDto = null;
        if (!_userSpecification.IsSatisfiedBy(userCreateDto)) return userUpdateDto;
        if (!_userRepository.ValidateInput(userCreateDto, false)) return userUpdateDto;

        var user = _userFactory.MapToUser(userCreateDto);
        userUpdateDto = _userFactory.MapToUserUpdateDto(_userRepository.Add(user));
        return userUpdateDto;
    }

    public void Update(UserUpdateDto userUpdateDto)
    {
        if (!_userSpecification.IsSatisfiedBy(userUpdateDto)) return;

        var existingUser = _userRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = userUpdateDto.Id,
            Key = "Equal"
        });

        if (!_userRepository.ValidateInput(userUpdateDto, true, existingUser)) return;

        var user = _userFactory.MapToUserFromUpdateDto(userUpdateDto);
        _userRepository.Update(user);

        _cacheService.Remove($"User:Id:{userUpdateDto.Id}");
        _cacheService.RemoveByPrefix("User:Filter:");
    }

    public void Delete(Guid id)
    {
        var existingUser = _userRepository.GetByElement(new FilterByItem
        {
            Field = "Id",
            Value = id,
            Key = "Equal"
        });

        if (existingUser is null) return;

        _userRepository.Delete(existingUser);

        _cacheService.Remove($"User:Id:{id}");
        _cacheService.RemoveByPrefix("User:Filter:");
    }
}
