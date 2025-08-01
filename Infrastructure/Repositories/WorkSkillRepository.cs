using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.FiltersModel;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Notifications;

namespace Infrastructure.Repositories
{
    public class WorkSkillRepository : BaseRepository<WorkSkill>, IWorkSkillRepository
    {
        private readonly NotificationContext _notificationContext;

        public WorkSkillRepository(AppDbContext context, IUnitOfWork unitOfWork, NotificationContext notificationContext)
            : base(context, unitOfWork, notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public WorkSkill GetByElement(FilterByItem filterByItem)
        {
            (WorkSkill workSkill, bool validadeIncludes) = GetElementEqual(filterByItem);

            if (validadeIncludes) return workSkill;

            if (filterByItem.Field == "Id" && workSkill is null)
                _notificationContext.AddNotification("Relação WorkSkill não encontrada.");

            return workSkill;
        }

        public FilterReturn<WorkSkill> GetFilter(FilterWorkSkillTable filter)
        {
            var filters = new Dictionary<string, string>();

            if (filter.WorkIdGuid.HasValue)
                filters.Add(nameof(WorkSkill.WorkId), filter.WorkIdGuid.ToString());

            if (filter.SkillIdGuid.HasValue)
                filters.Add(nameof(WorkSkill.SkillId), filter.SkillIdGuid.ToString());

            (var result, bool validadeIncludes) = GetFilters(filters, filter.PageSize, filter.PageNumber, filter.Includes);

            return result;
        }

        public bool ValidateInput(object dto, bool isUpdate, WorkSkill? existingWorkSkill = null)
        {
            var isValid = true;
            dynamic workSkillDto = dto;

            if (!IsWorkSkillCombinationUnique(existingWorkSkill, workSkillDto.WorkId, workSkillDto.SkillId))
                isValid = false;

            return isValid;
        }

        private bool IsWorkSkillCombinationUnique(WorkSkill? existingWorkSkill, Guid workId, Guid skillId)
        {
            if ((existingWorkSkill == null || existingWorkSkill.WorkId != workId || existingWorkSkill.SkillId != skillId)
                && GetByElement(new FilterByItem
                {
                    Field = "WorkId",
                    Value = workId.ToString(),
                    Key = "Equal"
                }) is WorkSkill existingByWork
                && existingByWork.SkillId == skillId)
            {
                _notificationContext.AddNotification("Essa combinação de WorkId e SkillId já está cadastrada.");
                return false;
            }

            return true;
        }
    }
}
