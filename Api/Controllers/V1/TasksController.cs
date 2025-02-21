using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Tasks.Commands.AddTask;
using TodoList.Application.Tasks.Commands.RemoveTask;
using TodoList.Application.Tasks.Commands.UpdateTask;
using TodoList.Application.Tasks.Queries.GetTasks;
using TodoList.Application.Tasks.Queries.GetTasksById;

namespace Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasks()
        {
            return await _mediator.Send(new GetTasksQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(Guid id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTask([FromBody] AddTaskCommand command)
        {
            var taskId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTask), new { id = taskId }, taskId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _mediator.Send(new RemoveTaskCommand(id));
            return NoContent();
        }
    }

}
