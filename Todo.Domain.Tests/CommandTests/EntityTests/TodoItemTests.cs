using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entitites;

namespace Todo.Domain.Tests.CommandTests.EntityTests
{
    [TestClass]

    public class TodoItemTests
    {
        private readonly TodoItem _validTodo = new("Titulo", "Lucas Cidade", DateTime.Now);
        [TestMethod]
        public void DadoUmNovoTodoOMesmoNaoPodeSerConcluido()
        {
            Assert.AreEqual(_validTodo.Done, false);
        }
        [TestMethod]
        public void DadoUmTodoValidoUtilizarMarkAsDoneERetornarTrue()
        {
            _validTodo.MarkAsDone();
            Assert.AreEqual(_validTodo.Done, true);
        }

        [TestMethod]
        public void DadoUmaTarefaComDoneTrueUtilizarMarkAsUndoneERetornarTrue()
        {
            _validTodo.MarkAsDone();
            _validTodo.MarkAsUndone();
            Assert.AreEqual(_validTodo.Done, false);
        }
    }
}