using Microsoft.EntityFrameworkCore;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.Interfaces.Repositories;
using MyFirstEF.Infrastructure.Data;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly MyFirstEFDbContext _context;

    public EmployeeRepository(MyFirstEFDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EmployeeDto>> GetHighSalaryEmployeesAsync()
    {
        var query = @"
            SELECT e.Id, e.Name, e.JoinedDate, d.Name AS DepartmentName
            FROM Employees e
            JOIN Salaries s ON e.Id = s.EmployeeId
            JOIN Departments d ON e.DepartmentId = d.Id
            WHERE s.Amount > 100 AND e.JoinedDate >= '2024-01-01'
        ";

        // Using ADO.NET here to manually map DTOs (since FromSqlRaw can't project to custom class easily)
        var result = new List<EmployeeDto>();
        using var connection = _context.Database.GetDbConnection();
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = query;

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new EmployeeDto
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                JoinedDate = reader.GetDateTime(2),
                DepartmentName = reader.GetString(3)
            });
        }

        return result;
    }

    public async Task AddWithSalaryAsync(CreateEmployeeWithSalaryDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                JoinedDate = dto.JoinedDate
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            var salary = new Salary
            {
                Id = Guid.NewGuid(),
                EmployeeId = employee.Id,
                Amount = dto.SalaryAmount
            };

            await _context.Salaries.AddAsync(salary);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}