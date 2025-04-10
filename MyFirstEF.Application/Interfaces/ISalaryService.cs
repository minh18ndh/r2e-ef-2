using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Interfaces;

public interface ISalaryService
{
    Task<IEnumerable<Salary>> GetAllAsync();
    Task<Salary?> GetByIdAsync(Guid id);
    Task AddAsync(Salary salary);
    Task UpdateAsync(Salary salary);
    Task DeleteAsync(Guid id);
}