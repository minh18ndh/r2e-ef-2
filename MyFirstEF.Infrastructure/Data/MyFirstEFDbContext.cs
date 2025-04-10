using Microsoft.EntityFrameworkCore;
using MyFirstEF.Domain.Entities;
using System.Reflection;

namespace MyFirstEF.Infrastructure.Data;

public class MyFirstEFDbContext : DbContext
{
    public MyFirstEFDbContext(DbContextOptions<MyFirstEFDbContext> options) : base(options) {}

    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    public virtual DbSet<Salary> Salaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all IEntityTypeConfiguration<T>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}