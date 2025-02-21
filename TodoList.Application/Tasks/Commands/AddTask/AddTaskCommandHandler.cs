using MediatR;
using TodoList.Domain.Repository;
using TodoList.Domain.Entity;       

namespace TodoList.Application.Tasks.Commands.AddTask;

public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, Guid>
{
    private readonly ITaskRepository _taskRepository;

    public AddTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Guid> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TodoTask(request.Title, request.Description);
        await _taskRepository.AddAsync(task);
        return task.Id;
    }
}
