using Microsoft.AspNetCore.Mvc;
using MyFirstEF.Application.Interfaces;
using MyFirstEF.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var emp = await _service.GetByIdAsync(id);
        return emp == null ? NotFound() : Ok(emp);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Employee employee)
    {
        await _service.AddAsync(employee);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Employee employee)
    {
        if (id != employee.Id) return BadRequest();
        await _service.UpdateAsync(employee);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("with-department")]
    public async Task<IActionResult> GetWithDepartment()
    {
        var result = await _service.GetAllAsync();

        var data = result.Select(e => new
        {
            EmployeeName = e.Name,
            DepartmentName = e.Department?.Name
        });

        return Ok(data);
    }

    [HttpGet("with-projects")]
    public async Task<IActionResult> GetWithProjects()
    {
        var result = await _service.GetAllAsync();

        var data = result.Select(e => new
        {
            EmployeeName = e.Name,
            Projects = e.ProjectEmployees?.Select(pe => pe.Project?.Name).ToList() ?? new List<string>()
        });

        return Ok(data);
    }

    [HttpGet("high-salary-rawsql")]
    public async Task<IActionResult> GetHighSalaryRaw()
    {
        var result = await _service.GetHighSalaryEmployeesRawAsync();
        return Ok(result);
    }
}