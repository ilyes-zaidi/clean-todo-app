using TodoList.Domain.Entity;

namespace TodoList.Domain.Repository;

public interface ITaskRepository
{
    
        Task<IEnumerable<TodoTask>> GetAllAsync();
        Task<TodoTask?> GetByIdAsync(Guid id);
        Task AddAsync(TodoTask task);
        Task UpdateAsync(TodoTask task);
        Task DeleteAsync(Guid id);
}