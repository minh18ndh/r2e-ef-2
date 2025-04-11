using MyFirstEF.Application.Interfaces.Services;
using MyFirstEF.Application.Interfaces.Repositories;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _employeeRepository.GetAllAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _employeeRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Employee employee)
    {
        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.SaveAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        _employeeRepository.Update(employee);
        await _employeeRepository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var emp = await _employeeRepository.GetByIdAsync(id);
        if (emp != null)
        {
            _employeeRepository.Delete(emp);
            await _employeeRepository.SaveAsync();
        }
    }

    public async Task<IEnumerable<Employee>> GetHighSalaryEmployeesRawAsync()
    {
        return await _employeeRepository.GetHighSalaryRawSqlAsync();
    }
}