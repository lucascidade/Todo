using Todo.Domain.Commands;

namespace Todo.Domain.Tests.CommandTest;

[TestClass]
public class CreateTodoCommandTest
{
    private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
    private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Tarefa 1", "lucas cidade", DateTime.Now);
    [TestMethod]
    public void DadoUmComandoInvalido()
    {
        _invalidCommand.Validate();
        //comaparar o valid com o false, ou seja, garantir que o Validade seja FALSO // resultado esperado é TRUE F+F = V
        Assert.AreEqual(_invalidCommand.Valid, false);
    }


    [TestMethod]
    public void DadoUmComandoValido()
    {
        _validCommand.Validate();
        Assert.AreEqual(_validCommand.Valid, true);
    }


}