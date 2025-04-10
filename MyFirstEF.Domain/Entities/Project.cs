namespace MyFirstEF.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<ProjectEmployee>  ProjectEmployees { get; set; } = new List<ProjectEmployee>();
}