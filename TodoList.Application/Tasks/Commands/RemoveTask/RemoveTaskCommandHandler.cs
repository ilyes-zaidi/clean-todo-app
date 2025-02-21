using MediatR;
using TodoList.Domain.Repository;
using TodoList.Domain.Entity;
using TodoList.Application.Common.Exceptions;

namespace TodoList.Application.Tasks.Commands.RemoveTask;

public class RemoveTaskCommandHandler : IRequestHandler<RemoveTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    public RemoveTaskCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        
        if (task == null)
        {
            throw new NotFoundException(nameof(TodoTask), request.Id);
        }

        await _taskRepository.DeleteAsync(request.Id);
    }
}
