using MediatR;

namespace TodoList.Application.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(Guid Id, string Title, string Description, bool IsCompleted) : IRequest;
