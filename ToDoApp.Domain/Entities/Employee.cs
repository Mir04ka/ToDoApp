namespace ToDoApp.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Speciality { get; set; } = string.Empty;
    public DateTime EmploymentDate { get; set; } = DateTime.UtcNow;

    public string? AvatarPath { get; set; }

    public ICollection<EmployeeTask> Tasks { get; set; } = new List<EmployeeTask>();

    public void AssignTask(EmployeeTask task)
    {
        Tasks.Add(task);
    }
}
