namespace MyFirstEF.Application.DTOs.Responses;

public class EmployeeWithProjectsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime JoinedDate { get; set; }

    public List<string> ProjectNames { get; set; } = new();
}