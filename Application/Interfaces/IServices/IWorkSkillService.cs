using Application.Dtos;
using Infrastructure.FiltersModel;

namespace Application.Interfaces.IServices;

public interface IWorkSkillService
{
    WorkSkillReadDto GetById(FilterWorkSkillById filterWorkSkillById);
    FilterReturn<WorkSkillReadDto> GetFilter(FilterWorkSkillTable filter);
    WorkSkillUpdateDto Add(WorkSkillCreateDto workSkillCreateDto);
    void Update(WorkSkillUpdateDto workSkillUpdateDto);
    void Delete(Guid id);
}
