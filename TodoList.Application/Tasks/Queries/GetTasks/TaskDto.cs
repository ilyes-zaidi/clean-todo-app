using TodoList.Application.Common.Mappings;
using TodoList.Domain.Entity;

namespace TodoList.Application.Tasks.Queries.GetTasks;

public class TaskDto : IMapFrom<TodoTask>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }

    public TaskDto() { 
    } 

    public TaskDto(Guid id, string title, string description, bool isCompleted)
    {
        Id = id;
        Title = title;
        Description = description;
        IsCompleted = isCompleted;
    }
}