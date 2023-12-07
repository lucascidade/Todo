using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class CreateTodoHandller
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Titulo 1", "Lucas Cidade", DateTime.Now);
        private readonly TodoHandler _handler = new TodoHandler(new FakeTodoRepository());
        [TestMethod]
        public void DadoUmCommandoInvalidoDeveIntrerromperAExecucao()
        {
            var result = (GerenicCommandResult)_handler.Handle(_invalidCommand);
            Assert.AreEqual(result.Sucess, false);
        }

        [TestMethod]
        public void DadoUmCommandoValidoDeveCriarATarefa()
        {
            var result = (GerenicCommandResult)_handler.Handle(_validCommand);
            Assert.AreEqual(result.Sucess, true);
        }
    }
}