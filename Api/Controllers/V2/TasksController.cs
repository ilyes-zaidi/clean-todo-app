using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Tasks.Commands.AddTask;
using TodoList.Application.Tasks.Queries.GetTasksById;
//Sample controller generated by AI 
namespace Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetTaskByIdQuery (id);
            var result = await _mediator.Send(query);
            
            // V2 adds additional metadata to the response
            var response = new
            {
                Data = result,
                Metadata = new
                {
                    Version = "2.0",
                    Timestamp = DateTimeOffset.UtcNow,
                    ApiEndpoint = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}"
                }
            };
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddTaskCommand command)
        {
            var result = await _mediator.Send(command);
            
            // V2 returns more detailed response
            var response = new
            {
                Id = result,
                Status = "Created",
                Timestamp = DateTimeOffset.UtcNow,
                Links = new
                {
                    Self = Url.Action(nameof(GetById), new { id = result, version = "2.0" }),
                    Collection = Url.Action("GetAll", new { version = "2.0" })
                }
            };
            
            return CreatedAtAction(nameof(GetById), new { id = result, version = "2.0" }, response);
        }

        // New endpoint in V2
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                Message = "This is a new endpoint available only in API V2",
                Notice = "Implementation pending - This is just a sample to demonstrate versioning"
            });
        }
    }
}
