namespace MyFirstEF.Application.DTOs.Responses;

public class ProjectEmployeeDto
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = default!;
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
    public bool Enable { get; set; }
}