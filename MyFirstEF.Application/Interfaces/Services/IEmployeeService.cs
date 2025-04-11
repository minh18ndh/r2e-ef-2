using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;

namespace MyFirstEF.Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByIdAsync(Guid id);
    Task AddAsync(CreateEmployeeDto dto);
    Task UpdateAsync(Guid id, CreateEmployeeDto dto);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<EmployeeDto>> GetEmployeesWithDepartmentAsync();
    Task<IEnumerable<EmployeeWithProjectsDto>> GetEmployeesWithProjectsAsync();
    Task<IEnumerable<EmployeeDto>> GetHighSalaryEmployeesAsync();
    Task AddWithSalaryAsync(CreateEmployeeWithSalaryDto dto);
}