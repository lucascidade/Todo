using System.Data;
using System.Net.Mail;
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
                return new GerenicCommandResult(false, "Tarefa invalida!", command.Notifications);
            //gera todo item
            var todo = new TodoItem(command.Title, command.User, command.Date);

            //salvar tarefa no banco
            _repository.Create(todo);

            return new GerenicCommandResult(true, "Tarefa Salva", todo);
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            //fail fast validation
            if (command.Invalid)
                return new GerenicCommandResult(false, "Ops, parece que sua tarefa esta incorreta", command.Notifications);
            //recupera todo do banco
            var todo = _repository.GetById(command.Id, command.User);
            //alterar titulo
            todo.UpdateTitle("titulo alterado");
            //sava no banco
            _repository.Update(todo);

            //retorna resultado
            return new GerenicCommandResult(true, "titulo alterado com sucsso", todo);
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            if (command.Invalid)
                return new GerenicCommandResult(false, "Ops, parece que sua tarefa está incorreta!", command.Notifications);
            var todo = _repository.GetById(command.Id, command.User);
            todo.MarkAsDone();
            _repository.Update(todo);
            return new GerenicCommandResult(true, "Tarefa Completa!", todo);
        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            if (command.Invalid)
                return new GerenicCommandResult(false, "Ops, sua tarefa está incorreta!", command.Notifications);

            var todo = _repository.GetById(command.Id, command.User);
            todo.MarkAsUndone();
            _repository.Update(todo);
            return new GerenicCommandResult(true, "Tarefa atualizada com sucesso!", todo);
        }
    }
}