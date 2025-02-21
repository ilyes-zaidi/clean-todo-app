using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoList.Domain.Repository;

namespace TodoList.Application.Tasks.Queries.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(ITaskRepository repository, IMapper mapper)
        {
            _taskRepository = repository;
            _mapper = mapper;
        }
        public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {

            var tasks = await _taskRepository.GetAllAsync();
            // WITHOUT USING mapper
            /*
            return tasks.Select(t => new TaskDto(t.Id, t.Title, t.Description, t.IsCompleted)).ToList();
            */

            return _mapper.Map<List<TaskDto>>(tasks);



        }
    }
}
