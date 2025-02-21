using MediatR;

namespace TodoList.Application.Tasks.Commands.AddTask;

public record AddTaskCommand(string Title, string Description) : IRequest<Guid>;
