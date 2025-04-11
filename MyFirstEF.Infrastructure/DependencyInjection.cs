using Microsoft.Extensions.DependencyInjection;
using MyFirstEF.Application.Interfaces.Repositories;
using MyFirstEF.Application.Interfaces.Services;
using MyFirstEF.Application.Services;
using MyFirstEF.Infrastructure.Repositories;

namespace MyFirstEF.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IProjectEmployeeRepository, ProjectEmployeeRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ISalaryService, SalaryService>();
        services.AddScoped<IProjectEmployeeService, ProjectEmployeeService>();

        return services;
    }
}