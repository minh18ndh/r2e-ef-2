using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Interfaces.Repositories;

public interface IProjectEmployeeRepository
{
    Task<IEnumerable<ProjectEmployee>> GetAllAsync();
    Task<ProjectEmployee?> GetByKeyAsync(Guid projectId, Guid employeeId);
    Task AddAsync(ProjectEmployee entity);
    Task UpdateAsync(ProjectEmployee entity);
    Task DeleteAsync(ProjectEmployee entity);
    Task SaveAsync();
}