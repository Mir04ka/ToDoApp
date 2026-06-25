namespace ToDoApp.Domain.Exceptions;

public class ValidationException : DomainException
{
    public ValidationException(string message)
        : base($"Validation failed: {message}")
    {
    }
}
