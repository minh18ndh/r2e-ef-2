using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;

namespace MyFirstEF.Application.Interfaces.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto?> GetByIdAsync(Guid id);
    Task AddAsync(CreateProjectDto dto);
    Task UpdateAsync(Guid id, CreateProjectDto dto);
    Task DeleteAsync(Guid id);
}
