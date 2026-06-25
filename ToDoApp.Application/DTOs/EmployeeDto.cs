namespace ToDoApp.Application.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Speciality { get; set; } = string.Empty;
    public string? AvatarPath { get; set; }
    public int TasksCount { get; set; }
}

public class CreateEmployeeDto
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Speciality { get; set; } = string.Empty;
    public string? AvatarPath { get; set; }
}
