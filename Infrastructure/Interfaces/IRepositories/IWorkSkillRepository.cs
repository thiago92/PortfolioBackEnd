using Domain.Entities;
using Infrastructure.FiltersModel;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface IWorkSkillRepository : IBaseRepository<WorkSkill>
    {
        WorkSkill GetByElement(FilterByItem filterByItem);
        FilterReturn<WorkSkill> GetFilter(FilterWorkSkillTable filter);
        bool ValidateInput(object dto, bool isUpdate, WorkSkill? existingWorkSkill = null);
    }
}
