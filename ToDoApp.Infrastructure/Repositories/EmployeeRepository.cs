using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infrastructure.Data;

namespace ToDoApp.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Tasks)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Employee>> SearchAsync(string? name, string? speciality, int? minAge, int? maxAge)
    {
        var query = _context.Employees
            .Include(e => e.Tasks)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));

        if (!string.IsNullOrWhiteSpace(speciality))
            query = query.Where(e => e.Speciality.ToLower().Contains(speciality.ToLower()));

        if (minAge.HasValue)
            query = query.Where(e => e.Age >= minAge.Value);

        if (maxAge.HasValue)
            query = query.Where(e => e.Age <= maxAge.Value);

        return await query.ToListAsync();
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}
