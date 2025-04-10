using MyFirstEF.Application.Interfaces;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class ProjectEmployeeService : IProjectEmployeeService
{
    private readonly IProjectEmployeeRepository _repository;

    public ProjectEmployeeService(IProjectEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProjectEmployee>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<ProjectEmployee?> GetByKeyAsync(Guid projectId, Guid employeeId) =>
        await _repository.GetByKeyAsync(projectId, employeeId);

    public async Task AddAsync(ProjectEmployee item)
    {
        await _repository.AddAsync(item);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(ProjectEmployee item)
    {
        await _repository.UpdateAsync(item);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid projectId, Guid employeeId)
    {
        var item = await _repository.GetByKeyAsync(projectId, employeeId);
        if (item != null)
        {
            await _repository.DeleteAsync(item);
            await _repository.SaveAsync();
        }
    }
}