using Microsoft.AspNetCore.Mvc;
using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.Interfaces.Services;

namespace MyFirstEF.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectEmployeesController : ControllerBase
{
    private readonly IProjectEmployeeService _service;

    public ProjectEmployeesController(IProjectEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectEmployeeDto>>> Get()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{projectId}/{employeeId}")]
    public async Task<ActionResult<ProjectEmployeeDto>> Get(Guid projectId, Guid employeeId)
    {
        var result = await _service.GetByKeyAsync(projectId, employeeId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProjectEmployeeDto dto)
    {
        await _service.AddAsync(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CreateProjectEmployeeDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete("{projectId}/{employeeId}")]
    public async Task<IActionResult> Delete(Guid projectId, Guid employeeId)
    {
        await _service.DeleteAsync(projectId, employeeId);
        return Ok();
    }
}