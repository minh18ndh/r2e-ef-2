using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Employee>> GetHighSalaryEmployeesRawAsync();

}