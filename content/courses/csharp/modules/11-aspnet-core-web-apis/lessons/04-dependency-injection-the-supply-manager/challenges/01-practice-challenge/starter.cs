using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}

interface ITodoRepository
{
    // Define methods
}

class TodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _todos = new()
    {
        new TodoItem { Id = 1, Title = "Learn DI", IsCompleted = false }
    };
    private int _nextId = 2;
    
    // Implement interface methods
}

// Register with DI
builder.Services.AddSingleton<ITodoRepository, TodoRepository>();

var app = builder.Build();

// Create endpoints with DI injection
app.MapGet("/api/todos", (ITodoRepository repo) =>
{
    // Use repo
});

// Implement other endpoints...

Console.WriteLine("DI-based Todo API Ready!");