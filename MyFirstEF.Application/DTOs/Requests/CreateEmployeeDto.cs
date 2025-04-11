namespace MyFirstEF.Application.DTOs.Requests;

public class CreateEmployeeDto
{
    public string Name { get; set; } = default!;
    public Guid DepartmentId { get; set; }
    public DateTime JoinedDate { get; set; }
}