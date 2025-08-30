using Application.Dtos;
using Infrastructure.FiltersModel;

namespace Application.Interfaces.IServices;

public interface ISkillService
{
    SkillReadDto GetById(FilterSkillById filterSkillById);
    FilterReturn<SkillReadDto> GetFilter(FilterSkillTable filter);
    SkillUpdateDto Add(SkillCreateDto skillCreateDto);
    void Update(SkillUpdateDto skillUpdateDto);
    void Delete(Guid id);
}
