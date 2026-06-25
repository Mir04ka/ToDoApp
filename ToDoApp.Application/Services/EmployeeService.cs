using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string? name, string? speciality, int? minAge, int? maxAge)
    {
        var employee = await _repository.SearchAsync(name, speciality, minAge, maxAge);

        return employee.Select(e => new EmployeeDto
        {
            Id = e.Id,
            Name = e.Name,
            Age = e.Age,
            Speciality = e.Speciality,
            AvatarPath = e.AvatarPath,
            TasksCount = e.Tasks.Count
        });
    }

    public async Task<EmployeeDto> GetByIdAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            throw new NotFoundException(nameof(Employee), id);

        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Age = employee.Age,
            Speciality = employee.Speciality,
            AvatarPath = employee.AvatarPath,
            TasksCount = employee.Tasks.Count
        };
    }

    public async Task CreateAsync(CreateEmployeeDto dto)
    {
        var employee = new Employee
        {
            Name = dto.Name,
            Age = dto.Age,
            Speciality = dto.Speciality,
            AvatarPath = dto.AvatarPath
        };

        await _repository.AddAsync(employee);
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
            throw new NotFoundException(nameof(Employee), id);

        await _repository.DeleteAsync(employee);
    }
}
