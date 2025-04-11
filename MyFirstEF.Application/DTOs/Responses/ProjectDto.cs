namespace MyFirstEF.Application.DTOs.Responses;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int MemberCount { get; set; }
}