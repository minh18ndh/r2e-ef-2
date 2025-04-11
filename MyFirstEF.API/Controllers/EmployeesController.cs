using Microsoft.AspNetCore.Mvc;
using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.Interfaces.Services;

namespace MyFirstEF.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
    {
        var employees = await _service.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> Get(Guid id)
    {
        var employee = await _service.GetByIdAsync(id);
        return employee == null ? NotFound() : Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEmployeeDto dto)
    {
        await _service.AddAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] CreateEmployeeDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("with-department")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetWithDepartment()
    {
        var result = await _service.GetEmployeesWithDepartmentAsync();
        return Ok(result);
    }

    [HttpGet("with-projects")]
    public async Task<ActionResult<IEnumerable<EmployeeWithProjectsDto>>> GetWithProjects()
    {
        var result = await _service.GetEmployeesWithProjectsAsync();
        return Ok(result);
    }

    [HttpGet("high-salary")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetHighSalary()
    {
        var result = await _service.GetHighSalaryEmployeesAsync();
        return Ok(result);
    }
}