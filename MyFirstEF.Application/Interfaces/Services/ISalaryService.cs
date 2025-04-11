using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;

namespace MyFirstEF.Application.Interfaces.Services;

public interface ISalaryService
{
    Task<IEnumerable<SalaryDto>> GetAllAsync();
    Task<SalaryDto?> GetByIdAsync(Guid id);
    Task AddAsync(CreateSalaryDto dto);
    Task UpdateAsync(Guid id, CreateSalaryDto dto);
    Task DeleteAsync(Guid id);
}