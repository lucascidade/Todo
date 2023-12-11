using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entitites;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests
{
    [TestClass]
    public class TodoQueryTests
    {

        private List<TodoItem> _items;
        public TodoQueryTests()
        {
            _items =
            [
                new TodoItem("tarefa 1", "usuario A", DateTime.Now),
                new TodoItem("tarefa 2", "lucascidade", DateTime.Now),
                new TodoItem("tarefa 3", "usuario B", DateTime.Now),
                new TodoItem("tarefa 4", "lucascidade", DateTime.Now),
                new TodoItem("tarefa 5", "usuario C", DateTime.Now),
            ];

        }
        [TestMethod]
        public void DadoConsultaDeveRetornarTarefasApenasDoUsuariolucascidade()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("lucascidade"));
            Assert.AreEqual(2, result.Count());
        }
    }
}