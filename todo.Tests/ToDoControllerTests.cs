using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.Controllers;
using Todo.context;
using Todo.entities;

namespace todo.Tests;

public class ToDoControllerTests
{
    private TodoContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new TodoContext(options);
    }

    [Fact]
    public void Get_ReturnsAllTodos()
    {
        using var context = CreateInMemoryContext();
        context.ToDos.AddRange(
            new ToDo { Title = "Buy milk", IsCompleted = false },
            new ToDo { Title = "Walk dog", IsCompleted = true }
        );
        context.SaveChanges();

        var controller = new ToDoController(context);
        var result = controller.Get() as OkObjectResult;

        Assert.NotNull(result);
        var todos = Assert.IsAssignableFrom<List<ToDo>>(result.Value);
        Assert.Equal(2, todos.Count);
    }

    [Fact]
    public void GetById_ReturnsCorrectTodo()
    {
        using var context = CreateInMemoryContext();
        context.ToDos.Add(new ToDo { Title = "Buy milk", IsCompleted = false });
        context.SaveChanges();
        var id = context.ToDos.First().Id;

        var controller = new ToDoController(context);
        var result = controller.GetById(id) as OkObjectResult;

        Assert.NotNull(result);
        var todo = Assert.IsType<ToDo>(result.Value);
        Assert.Equal("Buy milk", todo.Title);
    }

    [Fact]
    public void GetById_ReturnsNotFound_WhenMissing()
    {
        using var context = CreateInMemoryContext();
        var controller = new ToDoController(context);

        var result = controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Post_CreatesTodo_AndReturnsCreated()
    {
        using var context = CreateInMemoryContext();
        var controller = new ToDoController(context);
        var newTodo = new ToDo { Title = "New task", IsCompleted = false };

        var result = controller.Post(newTodo) as CreatedAtActionResult;

        Assert.NotNull(result);
        Assert.Equal(1, context.ToDos.Count());
        Assert.Equal("New task", context.ToDos.First().Title);
    }

    [Fact]
    public void Put_UpdatesTodo_AndReturnsNoContent()
    {
        using var context = CreateInMemoryContext();
        context.ToDos.Add(new ToDo { Title = "Old title", IsCompleted = false });
        context.SaveChanges();
        var id = context.ToDos.First().Id;

        var controller = new ToDoController(context);
        var updated = new ToDo { Id = id, Title = "New title", IsCompleted = true };

        var result = controller.Put(id, updated);

        Assert.IsType<NoContentResult>(result);
        var saved = context.ToDos.Find(id);
        Assert.Equal("New title", saved!.Title);
        Assert.True(saved.IsCompleted);
    }

    [Fact]
    public void Put_ReturnsNotFound_WhenMissing()
    {
        using var context = CreateInMemoryContext();
        var controller = new ToDoController(context);
        var todo = new ToDo { Id = 999, Title = "Ghost", IsCompleted = false };

        var result = controller.Put(999, todo);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_RemovesTodo_AndReturnsNoContent()
    {
        using var context = CreateInMemoryContext();
        context.ToDos.Add(new ToDo { Title = "To delete", IsCompleted = false });
        context.SaveChanges();
        var id = context.ToDos.First().Id;

        var controller = new ToDoController(context);
        var result = controller.Delete(id);

        Assert.IsType<NoContentResult>(result);
        Assert.Equal(0, context.ToDos.Count());
    }

    [Fact]
    public void Delete_ReturnsNotFound_WhenMissing()
    {
        using var context = CreateInMemoryContext();
        var controller = new ToDoController(context);

        var result = controller.Delete(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
