using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.Interfaces.Repositories;
using MyFirstEF.Application.Interfaces.Services;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _repository;

    public ProjectService(IRepository<Project> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync()
    {
        var projects = await _repository.GetAllAsync();

        return projects.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            MemberCount = p.ProjectEmployees?.Count ?? 0
        });
    }

    public async Task<ProjectDto?> GetByIdAsync(Guid id)
    {
        var p = await _repository.GetByIdAsync(id);

        return p == null ? null : new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            MemberCount = p.ProjectEmployees?.Count ?? 0
        };
    }

    public async Task AddAsync(CreateProjectDto dto)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = dto.Name
        };

        await _repository.AddAsync(project);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(Guid id, CreateProjectDto dto)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project == null) return;

        project.Name = dto.Name;

        _repository.Update(project);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await _repository.GetByIdAsync(id);
        if (project != null)
        {
            _repository.Delete(project);
            await _repository.SaveAsync();
        }
    }
}