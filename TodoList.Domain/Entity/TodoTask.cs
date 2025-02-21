namespace TodoList.Domain.Entity;

public class TodoTask(string title, string description)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public bool IsCompleted { get; private set; } = false;

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}