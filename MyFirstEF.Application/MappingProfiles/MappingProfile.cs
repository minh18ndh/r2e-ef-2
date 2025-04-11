using AutoMapper;
using MyFirstEF.Application.DTOs.Requests;
using MyFirstEF.Application.DTOs.Responses;
using MyFirstEF.Domain.Entities;

namespace MyFirstEF.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.EmployeeCount, opt => opt.MapFrom(src => src.Employees != null ? src.Employees.Count : 0));
        CreateMap<CreateDepartmentDto, Department>();

        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : "(none)"));
        CreateMap<CreateEmployeeDto, Employee>();

        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.MemberCount, opt => opt.MapFrom(src => src.ProjectEmployees != null ? src.ProjectEmployees.Count : 0));
        CreateMap<CreateProjectDto, Project>();

        CreateMap<Salary, SalaryDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : "(Unknown)"));
        CreateMap<CreateSalaryDto, Salary>();

        CreateMap<ProjectEmployee, ProjectEmployeeDto>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.Name : "(Unknown)"))
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.Name : "(Unknown)"));

        CreateMap<CreateProjectEmployeeDto, ProjectEmployee>();
    }
}