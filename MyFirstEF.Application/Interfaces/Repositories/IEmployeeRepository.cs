using MyFirstEF.Application.DTOs.Responses;

namespace MyFirstEF.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<EmployeeDto>> GetHighSalaryEmployeesAsync();
}