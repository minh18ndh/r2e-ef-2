using Microsoft.EntityFrameworkCore;
using MyFirstEF.Application.Interfaces;
using MyFirstEF.Domain.Entities;
using MyFirstEF.Infrastructure.Data;

namespace MyFirstEF.Infrastructure.Repositories;

public class ProjectEmployeeRepository : IProjectEmployeeRepository
{
    private readonly MyFirstEFDbContext _context;

    public ProjectEmployeeRepository(MyFirstEFDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectEmployee>> GetAllAsync() =>
        await _context.ProjectEmployees.ToListAsync();

    public async Task<ProjectEmployee?> GetByKeyAsync(Guid projectId, Guid employeeId) =>
        await _context.ProjectEmployees
                      .FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);

    public async Task AddAsync(ProjectEmployee entity) =>
        await _context.ProjectEmployees.AddAsync(entity);

    public Task UpdateAsync(ProjectEmployee entity)
    {
        _context.ProjectEmployees.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(ProjectEmployee entity)
    {
        _context.ProjectEmployees.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}