namespace MyFirstEF.Domain.Entities;

public class Department
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    // Navigation
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}