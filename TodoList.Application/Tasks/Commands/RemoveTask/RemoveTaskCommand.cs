using MediatR;

namespace TodoList.Application.Tasks.Commands.RemoveTask;

public record RemoveTaskCommand(Guid Id) : IRequest;
