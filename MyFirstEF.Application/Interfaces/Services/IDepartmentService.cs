using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;

namespace MyFirstEF.Application.Interfaces.Services;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto?> GetByIdAsync(Guid id);
    Task AddAsync(CreateDepartmentDto dto);
    Task UpdateAsync(Guid id, CreateDepartmentDto dto);
    Task DeleteAsync(Guid id);
}