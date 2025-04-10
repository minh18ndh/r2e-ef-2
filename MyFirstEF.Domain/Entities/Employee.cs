namespace MyFirstEF.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid DepartmentId { get; set; }
    public DateTime JoinedDate { get; set; }

    public Department? Department { get; set; }
    public Salary? Salary { get; set; }
    public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
}