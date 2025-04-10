using Microsoft.EntityFrameworkCore;
using MyFirstEF.Application.Interfaces;
using MyFirstEF.Domain.Entities;
using MyFirstEF.Infrastructure.Data;

namespace MyFirstEF.Infrastructure.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly MyFirstEFDbContext _context;

    public EmployeeRepository(MyFirstEFDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetHighSalaryRawSqlAsync()
    {
        return await _context.Employees
            .FromSqlRaw(@"
                SELECT e.*
                FROM Employees e
                JOIN Salaries s ON s.EmployeeId = e.Id
                WHERE s.Amount > 100 AND e.JoinedDate >= '2024-01-01'
            ")
            .ToListAsync();
    }
}