using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.Interfaces.Repositories;
using MyFirstEF.Application.Interfaces.Services;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class SalaryService : ISalaryService
{
    private readonly IRepository<Salary> _repository;

    public SalaryService(IRepository<Salary> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SalaryDto>> GetAllAsync()
    {
        var salaries = await _repository.GetAllAsync();

        return salaries.Select(s => new SalaryDto
        {
            Id = s.Id,
            EmployeeId = s.EmployeeId,
            EmployeeName = s.Employee?.Name ?? "(Unknown)",
            Amount = s.Amount
        });
    }

    public async Task<SalaryDto?> GetByIdAsync(Guid id)
    {
        var s = await _repository.GetByIdAsync(id);
        return s == null ? null : new SalaryDto
        {
            Id = s.Id,
            EmployeeId = s.EmployeeId,
            EmployeeName = s.Employee?.Name ?? "(Unknown)",
            Amount = s.Amount
        };
    }

    public async Task AddAsync(CreateSalaryDto dto)
    {
        var salary = new Salary
        {
            Id = Guid.NewGuid(),
            EmployeeId = dto.EmployeeId,
            Amount = dto.Amount
        };

        await _repository.AddAsync(salary);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(Guid id, CreateSalaryDto dto)
    {
        var salary = await _repository.GetByIdAsync(id);
        if (salary == null) return;

        salary.EmployeeId = dto.EmployeeId;
        salary.Amount = dto.Amount;

        _repository.Update(salary);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var salary = await _repository.GetByIdAsync(id);
        if (salary != null)
        {
            _repository.Delete(salary);
            await _repository.SaveAsync();
        }
    }
}