namespace ToDoApp.Domain.Entities;

public class EmployeeTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.ToDo;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public void MarkAsCompleted()
    {
        Status = Enums.TaskStatus.Completed;
    }
}
