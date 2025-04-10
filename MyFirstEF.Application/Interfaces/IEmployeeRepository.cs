using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetHighSalaryRawSqlAsync();
}