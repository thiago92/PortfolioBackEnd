using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly NotificationContext _notificationContext;

        public UserRepository(AppDbContext context, IUnitOfWork unitOfWork, NotificationContext notificationContext)
            : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public User GetByElement(FilterByItem filterByItem)
        {
            (User user, bool validIncludes) = GetElementEqual(filterByItem);

            if (validIncludes)
                return user;

            if (filterByItem.Field == "Id" && user is null)
                Notify("Usuário não registrado!");

            return user;
        }

        public FilterReturn<User> GetFilter(FilterUserTable filter)
        {
            var filters = BuildFiltersDictionary(filter);

            (var result, _) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);
            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, User existingUser = null)
        {
            dynamic userDto = dto;
            return ValidateEmailUniqueness(existingUser, userDto.Email);
        }

        private bool ValidateEmailUniqueness(User? existingUser, string email)
        {
            bool isEmailChanged = existingUser == null || existingUser.Email != email;
            bool emailAlreadyExists = GetByElement(new FilterByItem
            {
                Field = "Email",
                Value = email,
                Key = "Equal"
            }) is not null;

            if (isEmailChanged && emailAlreadyExists)
            {
                Notify("Esse email já está cadastrado.");
                return false;
            }

            return true;
        }

        private Dictionary<string, string> BuildFiltersDictionary(FilterUserTable filter)
        {
            var filters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(filter.UserNameContains))
                filters[nameof(filter.UserNameContains)] = filter.UserNameContains;

            if (!string.IsNullOrWhiteSpace(filter.EmailContains))
                filters[nameof(filter.EmailContains)] = filter.EmailContains;

            if (filter.CreatedDateBetweenDates.HasValue)
            {
                string date = filter.CreatedDateBetweenDates.Value.ToString("yyyy-MM-dd");
                filters["StartDate"] = date;
                filters["EndDate"] = date;
            }

            if (filter.RoleIdGuid.HasValue)
                filters[nameof(User.RoleId)] = filter.RoleIdGuid.Value.ToString();

            return filters;
        }

        private void Notify(string message)
        {
            _notificationContext.AddNotification(message);
        }
    }
}
