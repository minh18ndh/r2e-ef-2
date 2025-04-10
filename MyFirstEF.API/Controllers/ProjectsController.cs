using Microsoft.AspNetCore.Mvc;
using MyFirstEF.Application.Interfaces;
using MyFirstEF.Domain.Entities;
using MyFirstEF.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MyFirstEF.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;
    private readonly MyFirstEFDbContext _context;

    public ProjectsController(IProjectService service, MyFirstEFDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Project project)
    {
        await _service.AddAsync(project);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Project project)
    {
        if (id != project.Id) return BadRequest();
        await _service.UpdateAsync(project);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    // Transaction: Create project + assign multiple employees
    [HttpPost("create-with-employees")]
    public async Task<IActionResult> CreateWithEmployees([FromBody] CreateProjectWithEmployeesRequest request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Validate employee IDs
            var existingEmployees = await _context.Employees
                .Where(e => request.EmployeeIds.Contains(e.Id))
                .Select(e => e.Id)
                .ToListAsync();

            if (existingEmployees.Count != request.EmployeeIds.Count)
                return BadRequest("One or more employee IDs are invalid.");

            // Create new project
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // Link employees
            foreach (var empId in request.EmployeeIds)
            {
                _context.ProjectEmployees.Add(new ProjectEmployee
                {
                    ProjectId = project.Id,
                    EmployeeId = empId,
                    Enable = true
                });
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok("Project created with employees.");
        }
        catch
        {
            await transaction.RollbackAsync();
            return BadRequest("Transaction failed.");
        }
    }
}

// DTO for transactional endpoint
public class CreateProjectWithEmployeesRequest
{
    public string Name { get; set; } = default!;
    public List<Guid> EmployeeIds { get; set; } = new();
}