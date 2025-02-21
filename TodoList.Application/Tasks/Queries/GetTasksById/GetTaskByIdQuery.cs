using MediatR;
using TodoList.Application.Tasks.Queries.GetTasks;

namespace TodoList.Application.Tasks.Queries.GetTasksById;

public record GetTaskByIdQuery(Guid Id) : IRequest<TaskDto>;
