namespace MyFirstEF.Application.DTOs.Requests;

public class CreateSalaryDto
{
    public Guid EmployeeId { get; set; }
    public decimal Amount { get; set; }
}