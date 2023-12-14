
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entitites;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Repositories;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers
{

    [ApiController]
    [Authorize]
    [Route("v1/todos")]

    public class TodoController : ControllerBase
    {

        [HttpGet("")]
        public IEnumerable<TodoItem> GetAll(
        [FromServices] ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAll(user);
        }

        [HttpGet("done")]
        public IEnumerable<TodoItem> GetAllDone(
            [FromServices] ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAllDone(user);
        }

        [HttpGet("undone")]
        public IEnumerable<TodoItem> GetAllUndone(
            [FromServices] ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAllUndone(user);
        }

        [HttpGet("done/today")]
        public IEnumerable<TodoItem> GetDoneForToday(
            [FromServices] ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(
                user,
                DateTime.Now.Date,
                true
            );
        }


        [HttpGet("undone/today")]
        public IEnumerable<TodoItem> GetUndoneForToday(
            [FromServices] ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(
                user,
                DateTime.Now.Date,
                false
            );
        }


        [HttpGet("tomorrow")]
        public IEnumerable<TodoItem> GetDoneForTomorrow(
        [FromServices] ITodoRepository repository
)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(
                user,
                DateTime.Now.Date.AddDays(1),
                false
            );
        }
        [HttpGet("undone/tomorrow")]
        public IEnumerable<TodoItem> GetUndoneForTomorrow(
            [FromServices] ITodoRepository repository
        )
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(
                user,
                DateTime.Now.Date.AddDays(1),
                false
            );
        }
        [HttpPost("")]
        public GenericCommandResult Create(
                   [FromBody] CreateTodoCommand command,
                   [FromServices] TodoHandler handler
                   )
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }

        //UPDATE

        [HttpPut("")]
        public GenericCommandResult Update(
            [FromBody] UpdateTodoCommand command,
            [FromServices] TodoHandler handler
        )
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }
        [HttpPut("mark-as-done")]
        public GenericCommandResult MarkAsDone(
        [FromBody] MarkTodoAsDoneCommand command,
        [FromServices] TodoHandler handler
        )
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }
        [HttpPut("mark-as-undone")]
        public GenericCommandResult MarkAsUndone(
        [FromBody] MarkTodoAsUndoneCommand command,
        [FromServices] TodoHandler handler
        )
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }
    }
}