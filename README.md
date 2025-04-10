This section explains how **Raw SQL queries** and **Transactions** are handled in a clean way using **EF Core + Clean Architecture principles**.

---

## Raw SQL Handling

### Implementation:

- **Raw SQL is not placed in the controller** (to avoid infrastructure leakage).
- It is encapsulated in a **custom repository** called `EmployeeRepository` in the **Infrastructure** layer.

**Application/Interfaces/IEmployeeRepository.cs**
```csharp
Task<IEnumerable<Employee>> GetHighSalaryRawSqlAsync();
```

**Infrastructure/Repositories/EmployeeRepository.cs**
```csharp
public async Task<IEnumerable<Employee>> GetHighSalaryRawSqlAsync()
{
    return await _context.Employees
        .FromSqlRaw(@"
            SELECT e.*
            FROM Employees e
            JOIN Salaries s ON e.Id = s.EmployeeId
            WHERE s.Amount > 100 AND e.JoinedDate >= '2024-01-01'
        ").ToListAsync();
}
```

**Application/Services/EmployeeService.cs**
```csharp
public async Task<IEnumerable<Employee>> GetHighSalaryEmployeesRawAsync()
{
    return await _employeeRepository.GetHighSalaryRawSqlAsync();
}
```

**API/Controllers/EmployeesController.cs**
```csharp
[HttpGet("high-salary-raw")]
public async Task<IActionResult> GetHighSalaryRaw()
{
    var result = await _service.GetHighSalaryEmployeesRawAsync();
    return Ok(result);
}
```

**Benefit:**  
Separation of concerns, raw SQL is abstracted away from API logic.

---

## Transaction Handling

### Implementation:

- A POST endpoint receives a `Project` name and a list of employee IDs.
- Using `DbContext.Database.BeginTransactionAsync()`, a transaction is created.
- If all operations succeed, `CommitAsync()` is called.
- If any fail, the transaction is `Rollback()`ed.

**API/Controllers/ProjectsController.cs**
```csharp
[HttpPost("create-with-employees")]
public async Task<IActionResult> CreateWithEmployees([FromBody] CreateProjectWithEmployeesRequest request)
{
    using var transaction = await _context.Database.BeginTransactionAsync();

    try
    {
        var project = new Project { Id = Guid.NewGuid(), Name = request.Name };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

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
        return Ok("Project with employees created.");
    }
    catch
    {
        await transaction.RollbackAsync();
        return BadRequest("Transaction failed.");
    }
}
```

**DTO**
```csharp
public class CreateProjectWithEmployeesRequest
{
    public string Name { get; set; } = default!;
    public List<Guid> EmployeeIds { get; set; } = new();
}
```

**Benefit:**  
Ensures atomicity — either all entities are created, or none are.

---
