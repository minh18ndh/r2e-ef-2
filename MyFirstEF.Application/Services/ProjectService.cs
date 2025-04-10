using MyFirstEF.Application.Interfaces;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _repository;

    public ProjectService(IRepository<Project> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Project>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Project?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

    public async Task AddAsync(Project project)
    {
        await _repository.AddAsync(project);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(Project project)
    {
        _repository.Update(project);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity != null)
        {
            _repository.Delete(entity);
            await _repository.SaveAsync();
        }
    }
}