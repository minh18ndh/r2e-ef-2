using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;

namespace MyFirstEF.Application.Interfaces.Services;

public interface IProjectEmployeeService
{
    Task<IEnumerable<ProjectEmployeeDto>> GetAllAsync();
    Task<ProjectEmployeeDto?> GetByKeyAsync(Guid projectId, Guid employeeId);
    Task AddAsync(CreateProjectEmployeeDto dto);
    Task UpdateAsync(CreateProjectEmployeeDto dto);
    Task DeleteAsync(Guid projectId, Guid employeeId);
}