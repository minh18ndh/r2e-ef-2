using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.DTOs.Requests;

namespace MyFirstEF.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<EmployeeDto>> GetHighSalaryEmployeesAsync();
    Task AddWithSalaryAsync(CreateEmployeeWithSalaryDto dto);

}