using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string? name, string? speciality, int? minAge, int? maxAge);
    Task<EmployeeDto> GetByIdAsync(int id);
    Task CreateAsync(CreateEmployeeDto dto);
    Task DeleteAsync(int id);
}
