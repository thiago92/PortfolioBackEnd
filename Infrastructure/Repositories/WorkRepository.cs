using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;

namespace Infrastructure.Repositories
{
    public class WorkRepository : BaseRepository<Work>, IWorkRepository
    {
        private readonly NotificationContext _notificationContext;

        public WorkRepository(AppDbContext context, IUnitOfWork unitOfWork, NotificationContext notificationContext)
            : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public Work GetByElement(FilterByItem filterByItem)
        {
            (Work work, bool validadeIncludes) = GetElementEqual(filterByItem);

            if (validadeIncludes) return work;

            if (filterByItem.Field == "Id" && work is null)
                _notificationContext.AddNotification("Trabalho não encontrado!");

            return work;
        }

        public FilterReturn<Work> GetFilter(FilterWorkTable filter)
        {
            var filters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(filter.NameContains))
                filters.Add(nameof(filter.NameContains), filter.NameContains);

            if (!string.IsNullOrEmpty(filter.UrlContains))
                filters.Add(nameof(filter.UrlContains), filter.UrlContains);

            if (!string.IsNullOrEmpty(filter.ImageContains))
                filters.Add(nameof(filter.ImageContains), filter.ImageContains);

            if (!string.IsNullOrEmpty(filter.AltImageContains))
                filters.Add(nameof(filter.AltImageContains), filter.AltImageContains);

            if (filter.IsFreelanceEqual.HasValue)
                filters.Add(nameof(filter.IsFreelanceEqual), filter.IsFreelanceEqual.Value.ToString());

            if (filter.CreatedDateBetweenDates.HasValue)
            {
                var date = filter.CreatedDateBetweenDates.Value;
                filters.Add("StartDate", date.ToString("yyyy-MM-dd"));
                filters.Add("EndDate", date.ToString("yyyy-MM-dd"));
            }

            (var result, bool validadeIncludes) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);
            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, Work? existingWork = null)
        {
            var isValid = true;
            dynamic workDto = dto;

            if (!IsNameUnique(existingWork, workDto.Name)) isValid = false;

            return isValid;
        }

        private bool IsNameUnique(Work? existingWork, string name)
        {
            var workWithSameName = GetByElement(new FilterByItem
            {
                Field = "Name",
                Value = name,
                Key = "Equal"
            });

            var isSameAsExisting = existingWork != null && existingWork.Name == name;

            if (!isSameAsExisting && workWithSameName is not null)
            {
                _notificationContext.AddNotification("Esse nome de trabalho já está em uso.");
                return false;
            }

            return true;
        }
    }
}
