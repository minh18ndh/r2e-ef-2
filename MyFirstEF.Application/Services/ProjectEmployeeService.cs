using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.Interfaces.Repositories;
using MyFirstEF.Application.Interfaces.Services;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class ProjectEmployeeService : IProjectEmployeeService
{
    private readonly IProjectEmployeeRepository _repository;

    public ProjectEmployeeService(IProjectEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProjectEmployeeDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();

        return items.Select(pe => new ProjectEmployeeDto
        {
            ProjectId = pe.ProjectId,
            ProjectName = pe.Project?.Name ?? "(Unknown)",
            EmployeeId = pe.EmployeeId,
            EmployeeName = pe.Employee?.Name ?? "(Unknown)",
            Enable = pe.Enable
        });
    }

    public async Task<ProjectEmployeeDto?> GetByKeyAsync(Guid projectId, Guid employeeId)
    {
        var pe = await _repository.GetByKeyAsync(projectId, employeeId);
        return pe == null ? null : new ProjectEmployeeDto
        {
            ProjectId = pe.ProjectId,
            ProjectName = pe.Project?.Name ?? "(Unknown)",
            EmployeeId = pe.EmployeeId,
            EmployeeName = pe.Employee?.Name ?? "(Unknown)",
            Enable = pe.Enable
        };
    }

    public async Task AddAsync(CreateProjectEmployeeDto dto)
    {
        var item = new ProjectEmployee
        {
            ProjectId = dto.ProjectId,
            EmployeeId = dto.EmployeeId,
            Enable = dto.Enable
        };

        await _repository.AddAsync(item);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(CreateProjectEmployeeDto dto)
    {
        var pe = await _repository.GetByKeyAsync(dto.ProjectId, dto.EmployeeId);
        if (pe == null) return;

        pe.Enable = dto.Enable;

        await _repository.UpdateAsync(pe);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid projectId, Guid employeeId)
    {
        var item = await _repository.GetByKeyAsync(projectId, employeeId);
        if (item != null)
        {
            await _repository.DeleteAsync(item);
            await _repository.SaveAsync();
        }
    }
}