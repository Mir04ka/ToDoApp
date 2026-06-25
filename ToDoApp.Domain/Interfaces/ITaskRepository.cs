using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces;

public interface ITaskRepository
{
    Task<EmployeeTask?> GetByIdAsync(int id);

    Task<IEnumerable<EmployeeTask>> SearchTasksAsync(int? employeeId, string? titleQuery, Enums.TaskStatus? status);

    Task AddAsync(EmployeeTask task);
    Task UpdateAsync(EmployeeTask task);
    Task DeleteAsync(EmployeeTask task);
}
