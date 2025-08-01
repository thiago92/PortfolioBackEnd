using Domain.Entities;
using Infrastructure.FiltersModel;

namespace Infrastructure.Interfaces.IRepositories
{
    public interface ISkillRepository : IBaseRepository<Skill>
    {
        Skill GetByElement(FilterByItem filterByItem);
        FilterReturn<Skill> GetFilter(FilterSkillTable filter);
        bool ValidateInput(object dto, bool isUpdate, Skill? existingSkill = null);
    }
}
