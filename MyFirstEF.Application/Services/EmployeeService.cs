using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Application.Interfaces.Repositories;
using MyFirstEF.Application.Interfaces.Services;
using MyFirstEF.Domain.Entities;
using AutoMapper;

namespace MyFirstEF.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _repository;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _customRepo;

    public EmployeeService(IRepository<Employee> repository, IEmployeeRepository customRepo, IMapper mapper)
    {
        _repository = repository;
        _customRepo = customRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _repository.GetAllAsync();

        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            Name = e.Name,
            DepartmentName = e.Department?.Name ?? "(none)",
            JoinedDate = e.JoinedDate
        });
    }

    public async Task<EmployeeDto?> GetByIdAsync(Guid id)
    {
        var e = await _repository.GetByIdAsync(id);

        return e == null ? null : new EmployeeDto
        {
            Id = e.Id,
            Name = e.Name,
            DepartmentName = e.Department?.Name ?? "(none)",
            JoinedDate = e.JoinedDate
        };
    }

    public async Task AddAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            DepartmentId = dto.DepartmentId,
            JoinedDate = dto.JoinedDate
        };

        await _repository.AddAsync(employee);
        await _repository.SaveAsync();
    }

    public async Task UpdateAsync(Guid id, CreateEmployeeDto dto)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null) return;

        employee.Name = dto.Name;
        employee.DepartmentId = dto.DepartmentId;
        employee.JoinedDate = dto.JoinedDate;

        _repository.Update(employee);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee != null)
        {
            _repository.Delete(employee);
            await _repository.SaveAsync();
        }
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesWithDepartmentAsync()
    {
        var employees = await _repository.GetAllAsync();

        var filtered = employees
            .Where(e => e.Department != null) // Inner join
            .ToList();

        return _mapper.Map<IEnumerable<EmployeeDto>>(filtered);
    }

    public async Task<IEnumerable<EmployeeWithProjectsDto>> GetEmployeesWithProjectsAsync()
    {
        var employees = await _repository.GetAllAsync();

        var result = employees.Select(e => new EmployeeWithProjectsDto
        {
            Id = e.Id,
            Name = e.Name,
            JoinedDate = e.JoinedDate,
            ProjectNames = e.ProjectEmployees?
                .Select(pe => pe.Project?.Name ?? "(Unknown)")
                .ToList() ?? new List<string>()
        });

        return result;
    }

    public async Task<IEnumerable<EmployeeDto>> GetHighSalaryEmployeesAsync()
    {
        return await _customRepo.GetHighSalaryEmployeesAsync();
    }

    public async Task AddWithSalaryAsync(CreateEmployeeWithSalaryDto dto)
    {
        await _customRepo.AddWithSalaryAsync(dto);
    }
}