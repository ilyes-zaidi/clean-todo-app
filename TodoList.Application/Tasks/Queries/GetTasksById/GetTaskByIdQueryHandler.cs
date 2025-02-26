using AutoMapper;
using MediatR;
using TodoList.Domain.Repository;
using TodoList.Domain.Entity;
using TodoList.Application.Common.Exceptions;
using TodoList.Application.Tasks.Queries.GetTasks;

namespace TodoList.Application.Tasks.Queries.GetTasksById;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    
    public GetTaskByIdQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        
        if (task == null)
        {
            throw new NotFoundException(nameof(TodoTask), request.Id);
        }


        return _mapper.Map<TaskDto>(task);
    }
}
