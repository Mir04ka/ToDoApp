using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Exceptions;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public TaskService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
    {
        _taskRepository = taskRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<TaskDto>> SearchTasksAsync(int? employeeId, string? titleQuery, Domain.Enums.TaskStatus? status)
    {
        var tasks = await _taskRepository.SearchTasksAsync(employeeId, titleQuery, status);

        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            StatusName = t.Status.ToString(),
            EmployeeId = t.EmployeeId,
            EmployeeName = t.Employee?.Name ?? "Unknown"
        });
    }

    public async Task CreateAsync(CreateTaskDto dto)
    {
        var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
        if (employee == null)
            throw new NotFoundException(nameof(Employee), dto.EmployeeId);

        var task = new EmployeeTask
        {
            Title = dto.Title,
            Description = dto.Description,
            EmployeeId = dto.EmployeeId,
        };

        await _taskRepository.AddAsync(task);
    }

    public async Task MarkAsCompletedAsync(int taskId)
    {
        var task = await _taskRepository.GetByIdAsync(taskId);
        if (task == null)
            throw new NotFoundException(nameof(EmployeeTask), taskId);

        task.MarkAsCompleted();
        await _taskRepository.UpdateAsync(task);
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new NotFoundException(nameof(EmployeeTask), id);

        await _taskRepository.DeleteAsync(task);
    }
}
