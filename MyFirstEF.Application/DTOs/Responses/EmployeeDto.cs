namespace MyFirstEF.Application.DTOs.Responses;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string DepartmentName { get; set; } = default!;
    public DateTime JoinedDate { get; set; }
}