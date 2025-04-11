using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Interfaces.Services;

public interface IProjectEmployeeService
{
    Task<IEnumerable<ProjectEmployee>> GetAllAsync();
    Task<ProjectEmployee?> GetByKeyAsync(Guid projectId, Guid employeeId);
    Task AddAsync(ProjectEmployee item);
    Task UpdateAsync(ProjectEmployee item);
    Task DeleteAsync(Guid projectId, Guid employeeId);
}