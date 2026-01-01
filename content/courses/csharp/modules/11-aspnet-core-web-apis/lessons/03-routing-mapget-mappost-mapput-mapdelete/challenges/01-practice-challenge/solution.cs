using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}

var tasks = new List<TaskItem>
{
    new TaskItem { Id = 1, Title = "Learn ASP.NET", Description = "Study web APIs", IsCompleted = false },
    new TaskItem { Id = 2, Title = "Build project", Description = "Create todo API", IsCompleted = false }
};

int nextId = 3;

app.MapGet("/api/tasks", () => tasks);

app.MapGet("/api/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    return task is not null ? Results.Ok(task) : Results.NotFound();
});

app.MapPost("/api/tasks", (TaskItem task) =>
{
    task.Id = nextId++;
    tasks.Add(task);
    return Results.Created($"/api/tasks/{task.Id}", task);
});

app.MapPut("/api/tasks/{id}", (int id, TaskItem updatedTask) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task is null) return Results.NotFound();
    
    task.Title = updatedTask.Title;
    task.Description = updatedTask.Description;
    task.IsCompleted = updatedTask.IsCompleted;
    return Results.Ok(task);
});

app.MapDelete("/api/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task is null) return Results.NotFound();
    
    tasks.Remove(task);
    return Results.NoContent();
});

Console.WriteLine("Task API Ready!");
Console.WriteLine("CRUD Endpoints:");
Console.WriteLine("  GET    /api/tasks");
Console.WriteLine("  GET    /api/tasks/{id}");
Console.WriteLine("  POST   /api/tasks");
Console.WriteLine("  PUT    /api/tasks/{id}");
Console.WriteLine("  DELETE /api/tasks/{id}");