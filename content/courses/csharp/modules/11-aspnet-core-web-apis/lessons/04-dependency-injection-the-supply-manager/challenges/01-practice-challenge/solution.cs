using Microsoft.AspNetCore.Builder;
using System;
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
    List<TodoItem> GetAll();
    TodoItem? GetById(int id);
    void Add(TodoItem item);
    void Update(int id, TodoItem item);
    void Delete(int id);
}

class TodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _todos = new()
    {
        new TodoItem { Id = 1, Title = "Learn DI", IsCompleted = false }
    };
    private int _nextId = 2;
    
    public List<TodoItem> GetAll() => _todos;
    
    public TodoItem? GetById(int id) => _todos.FirstOrDefault(t => t.Id == id);
    
    public void Add(TodoItem item)
    {
        item.Id = _nextId++;
        _todos.Add(item);
    }
    
    public void Update(int id, TodoItem item)
    {
        var todo = GetById(id);
        if (todo is not null)
        {
            todo.Title = item.Title;
            todo.IsCompleted = item.IsCompleted;
        }
    }
    
    public void Delete(int id)
    {
        var todo = GetById(id);
        if (todo is not null) _todos.Remove(todo);
    }
}

builder.Services.AddSingleton<ITodoRepository, TodoRepository>();

var app = builder.Build();

app.MapGet("/api/todos", (ITodoRepository repo) => repo.GetAll());

app.MapGet("/api/todos/{id}", (int id, ITodoRepository repo) =>
{
    var todo = repo.GetById(id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

app.MapPost("/api/todos", (TodoItem item, ITodoRepository repo) =>
{
    repo.Add(item);
    return Results.Created($"/api/todos/{item.Id}", item);
});

app.MapPut("/api/todos/{id}", (int id, TodoItem item, ITodoRepository repo) =>
{
    var existing = repo.GetById(id);
    if (existing is null) return Results.NotFound();
    repo.Update(id, item);
    return Results.Ok(existing);
});

app.MapDelete("/api/todos/{id}", (int id, ITodoRepository repo) =>
{
    var existing = repo.GetById(id);
    if (existing is null) return Results.NotFound();
    repo.Delete(id);
    return Results.NoContent();
});

Console.WriteLine("DI-based Todo API Ready!");
Console.WriteLine("Using Dependency Injection for clean architecture!");