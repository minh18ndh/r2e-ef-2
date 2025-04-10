using MyFirstEF.Application.Interfaces;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class SalaryService : ISalaryService
{
    private readonly IRepository<Salary> _repository;

    public SalaryService(IRepository<Salary> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Salary>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Salary?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

    public async Task AddAsync(Salary salary)
    {
        await _repository.AddAsync(salary);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(Salary salary)
    {
        _repository.Update(salary);
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