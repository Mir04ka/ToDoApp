using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int id);

    Task<IEnumerable<Employee>> SearchAsync(string? name, string? speciality, int? minAge, int? maxAge);

    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Employee employee);
}
