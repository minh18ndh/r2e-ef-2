using Microsoft.AspNetCore.Mvc;
using MyFirstEF.Application.Interfaces.Services;
using MyFirstEF.Domain.Entities;

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
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpGet("{projectId}/{employeeId}")]
    public async Task<IActionResult> Get(Guid projectId, Guid employeeId)
    {
        var item = await _service.GetByKeyAsync(projectId, employeeId);
        return item == null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProjectEmployee item)
    {
        await _service.AddAsync(item);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProjectEmployee item)
    {
        await _service.UpdateAsync(item);
        return Ok();
    }

    [HttpDelete("{projectId}/{employeeId}")]
    public async Task<IActionResult> Delete(Guid projectId, Guid employeeId)
    {
        await _service.DeleteAsync(projectId, employeeId);
        return Ok();
    }
}