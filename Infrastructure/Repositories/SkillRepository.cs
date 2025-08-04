using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;

namespace Infrastructure.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        private readonly NotificationContext _notificationContext;

        public SkillRepository(
            AppDbContext context,
            IUnitOfWork unitOfWork,
            NotificationContext notificationContext
        ) : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public Skill GetByElement(FilterByItem filterByItem)
        {
            (Skill skill, bool validIncludes) = GetElementEqual(filterByItem);

            if (validIncludes) return skill;

            if (filterByItem.Field == "Id" && skill is null)
                _notificationContext.AddNotification("Habilidade não encontrada!");

            return skill;
        }

        public FilterReturn<Skill> GetFilter(FilterSkillTable filter)
        {
            var filters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(filter.NameContains))
                filters.Add(nameof(filter.NameContains), filter.NameContains);

            if (filter.CreatedDateBetweenDates.HasValue)
            {
                var date = filter.CreatedDateBetweenDates.Value;
                filters.Add("StartDate", date.ToString("yyyy-MM-dd"));
                filters.Add("EndDate", date.ToString("yyyy-MM-dd"));
            }

            if (filter.ValueEqual.HasValue)
                filters.Add(nameof(filter.ValueEqual), filter.ValueEqual.Value.ToString());

            (var result, _) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);
            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, Skill? existingSkill = null)
        {
            if (dto == null)
            {
                return false;
            }

            dynamic skillDto = dto;

            bool isValid = true;

            if (!IsNameUnique(skillDto.Name, existingSkill))
                isValid = false;

            return isValid;
        }

        private bool IsNameUnique(string name, Skill? existingSkill)
        {
            var existing = GetByElement(new FilterByItem
            {
                Field = "Name",
                Value = name,
                Key = "Equal"
            });

            bool nameInUse = existing != null;
            bool isSameName = existingSkill != null && existingSkill.Name == name;

            if (!isSameName && nameInUse)
            {
                _notificationContext.AddNotification("Essa habilidade já está cadastrada.");
                return false;
            }

            return true;
        }
    }
}
