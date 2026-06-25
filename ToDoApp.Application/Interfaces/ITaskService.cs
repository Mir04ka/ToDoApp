using ToDoApp.Application.DTOs;
using TaskStatus = ToDoApp.Domain.Enums.TaskStatus;

namespace ToDoApp.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> SearchTasksAsync(int? employeeId, string? titleQuery, TaskStatus? status);
    Task CreateAsync(CreateTaskDto dto);
    Task MarkAsCompletedAsync(int taskId);
    Task DeleteAsync(int id);
}
