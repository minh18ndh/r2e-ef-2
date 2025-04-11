namespace MyFirstEF.Application.DTOs.Requests;

public class CreateEmployeeWithSalaryDto
{
    public string Name { get; set; } = default!;
    public Guid DepartmentId { get; set; }
    public DateTime JoinedDate { get; set; }
    public decimal SalaryAmount { get; set; }
}