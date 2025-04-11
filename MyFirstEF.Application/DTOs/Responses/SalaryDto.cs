namespace MyFirstEF.Application.DTOs.Responses;

public class SalaryDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
    public decimal Amount { get; set; }
}