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
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>
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
                return new GenericCommandResult(false, "Tarefa invalida!", command.Notifications);
            //gera todo item
            var todo = new TodoItem(command.Title, command.User, command.Date);

            //salvar tarefa no banco
            _repository.Create(todo);

            return new GenericCommandResult(true, "Tarefa Salva", todo);
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            //fail fast validation
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa esta incorreta", command.Notifications);
            //recupera todo do banco
            var todo = _repository.GetById(command.Id, command.User);
            //alterar titulo
            todo.UpdateTitle("titulo alterado");
            //sava no banco
            _repository.Update(todo);

            //retorna resultado
            return new GenericCommandResult(true, "titulo alterado com sucsso", todo);
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está incorreta!", command.Notifications);
            var todo = _repository.GetById(command.Id, command.User);
            todo.MarkAsDone();
            _repository.Update(todo);
            return new GenericCommandResult(true, "Tarefa Completa!", todo);
        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            if (command.Invalid)
                return new GenericCommandResult(false, "Ops, sua tarefa está incorreta!", command.Notifications);

            var todo = _repository.GetById(command.Id, command.User);
            todo.MarkAsUndone();
            _repository.Update(todo);
            return new GenericCommandResult(true, "Tarefa atualizada com sucesso!", todo);
        }
    }
}