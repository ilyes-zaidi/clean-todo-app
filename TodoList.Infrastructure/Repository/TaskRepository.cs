using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entity;
using TodoList.Domain.Repository;
using TodoList.Infrastructure.Data;

namespace TodoList.Infrastructure.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoTask>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TodoTask?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task AddAsync(TodoTask task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoTask task)
    {
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await GetByIdAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
