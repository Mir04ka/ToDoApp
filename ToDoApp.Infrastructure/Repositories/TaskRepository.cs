using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infrastructure.Data;

namespace ToDoApp.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeTask?> GetByIdAsync(int id)
    {
        return await _context.EmployeeTasks
            .Include(t => t.Employee)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<EmployeeTask>> SearchTasksAsync(int? employeeId, string? titleQuery, Domain.Enums.TaskStatus? status)
    {
        var query = _context.EmployeeTasks
            .AsNoTracking()
            .AsQueryable();

        if (employeeId.HasValue)
            query = query.Where(t => t.EmployeeId == employeeId.Value);

        if (!string.IsNullOrWhiteSpace(titleQuery))
            query = query.Where(t => t.Title.ToLower().Contains(titleQuery.ToLower()));

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        return await query.ToListAsync();
    }

    public async Task AddAsync(EmployeeTask task)
    {
        await _context.EmployeeTasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(EmployeeTask task)
    {
        _context.EmployeeTasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(EmployeeTask task)
    {
        _context.EmployeeTasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}
