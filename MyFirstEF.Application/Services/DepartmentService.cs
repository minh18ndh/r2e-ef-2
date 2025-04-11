using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.Interfaces.Repositories;
using MyFirstEF.Application.Interfaces.Services;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IRepository<Department> _repository;

    public DepartmentService(IRepository<Department> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        var departments = await _repository.GetAllAsync();

        return departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            EmployeeCount = d.Employees?.Count ?? 0
        });
    }

    public async Task<DepartmentDto?> GetByIdAsync(Guid id)
    {
        var d = await _repository.GetByIdAsync(id);
        return d == null ? null : new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            EmployeeCount = d.Employees?.Count ?? 0
        };
    }

    public async Task AddAsync(CreateDepartmentDto dto)
    {
        var department = new Department
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await _repository.AddAsync(department);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(Guid id, CreateDepartmentDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return;

        entity.Name = dto.Name;
        _repository.Update(entity);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity != null)
        {
            _repository.Delete(entity);
            await _repository.SaveAsync();
        }
    }
}