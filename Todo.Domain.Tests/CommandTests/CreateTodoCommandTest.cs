using Todo.Domain.Commands;

namespace Todo.Domain.Tests.CommandTest;

[TestClass]
public class CreateTodoCommandTest
{
    [TestMethod]
    public void DadoUmComandoInvalido()
    {
        var command = new CreateTodoCommand("", "", DateTime.Now);
        command.Validate();
        //comaparar o valid com o false, ou seja, garantir que o Validade seja FALSO // resultado esperado é TRUE F+F = V
        Assert.AreEqual(command.Valid, false);
    }


    [TestMethod]
    public void DadoUmComandoValido()
    {
        var command = new CreateTodoCommand("Tarefa", "lucas cidade", DateTime.Now);
        command.Validate();
        Assert.AreEqual(command.Valid, true);
    }


}