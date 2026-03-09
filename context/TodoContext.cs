using Microsoft.EntityFrameworkCore;
using Todo.entities;

namespace Todo.context;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<ToDo> ToDos { get; set; } = null!;
}