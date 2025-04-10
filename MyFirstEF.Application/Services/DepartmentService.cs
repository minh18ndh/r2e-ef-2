using MyFirstEF.Application.Interfaces;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> _repository;

    public DepartmentService(IRepository<Department> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Department>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<Department?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

    public async Task AddAsync(Department department)
    {
        await _repository.AddAsync(department);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(Department department)
    {
        _repository.Update(department);
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