using Application.Dtos;
using Infrastructure.FiltersModel;

namespace Application.Interfaces.IServices;

public interface IWorkService
{
    WorkReadDto GetById(FilterWorkById filterWorkById);
    FilterReturn<WorkReadDto> GetFilter(FilterWorkTable filter);
    WorkUpdateDto Add(WorkCreateDto workCreateDto);
    void Update(WorkUpdateDto workUpdateDto);
    void Delete(Guid id);
}
