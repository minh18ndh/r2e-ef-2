using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.Interfaces.Repositories;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetHighSalaryRawSqlAsync();
}