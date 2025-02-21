using MediatR;
using TodoList.Domain.Repository;
using TodoList.Domain.Entity;
using TodoList.Application.Common.Exceptions;

namespace TodoList.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        
        if (task == null)
        {
            throw new NotFoundException(nameof(TodoTask), request.Id);
        }

        // Create a new task with updated properties
        var updatedTask = new TodoTask(request.Title, request.Description)
        {
            Id = request.Id
        };
        
        if (request.IsCompleted)
        {
            updatedTask.MarkAsCompleted();
        }

        await _taskRepository.UpdateAsync(updatedTask);
    }
}
