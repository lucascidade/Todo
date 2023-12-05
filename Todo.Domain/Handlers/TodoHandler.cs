using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entitites;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler :
        Notifiable,
        IHandler<CreateTodoCommand>
    {
        private readonly ITodoRepository _repository;
        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CreateTodoCommand command)
        {
            //FAIL FAST VALIDATION
            command.Validate();
            if (command.Invalid)
                return new GerenicCommandResult(false, "Tarefa invalida!", command.Notifications);
            //gera todo item
            var todo = new TodoItem(command.Title, command.User, command.Date);

            //salvar tarefa no banco
            _repository.Create(todo);

            return new GerenicCommandResult(true, "Tarefa Salva", todo);
        }
    }
}