namespace ToDoApp.Application.Interfaces;

public interface IAuthService
{
    // Returns JWT token if successful, null if it's not
    Task<string?> LoginAsync(string username, string password);
}
