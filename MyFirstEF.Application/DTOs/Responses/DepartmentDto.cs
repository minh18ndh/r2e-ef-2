namespace MyFirstEF.Application.DTOs.Responses;

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int EmployeeCount { get; set; }
}