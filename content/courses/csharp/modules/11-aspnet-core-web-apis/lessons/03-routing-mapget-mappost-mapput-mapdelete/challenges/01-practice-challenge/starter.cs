using Microsoft.AspNetCore.Builder;
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

// GET all
app.MapGet("/api/tasks", () => tasks);

// GET by ID
app.MapGet("/api/tasks/{id}", (int id) =>
{
    // Find and return task or NotFound
});

// POST - Create
app.MapPost("/api/tasks", (TaskItem task) =>
{
    // Assign ID, add to list, return Created
});

// PUT - Update
app.MapPut("/api/tasks/{id}", (int id, TaskItem updatedTask) =>
{
    // Find, update properties, return Ok or NotFound
});

// DELETE
app.MapDelete("/api/tasks/{id}", (int id) =>
{
    // Find, remove, return NoContent or NotFound
});

Console.WriteLine("Task API Ready!");