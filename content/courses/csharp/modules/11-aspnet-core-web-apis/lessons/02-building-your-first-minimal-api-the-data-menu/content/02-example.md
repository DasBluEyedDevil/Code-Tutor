---
type: "EXAMPLE"
title: "Code Example"
---

This example demonstrates the concepts in action.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();  // .NET 9 OpenAPI
var app = builder.Build();
app.MapOpenApi();

// IN-MEMORY DATA (simulates database)
class Todo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool IsComplete { get; set; }
}

var todos = new List<Todo>
{
    new Todo { Id = 1, Title = "Learn C#", IsComplete = true },
    new Todo { Id = 2, Title = "Build API", IsComplete = false },
    new Todo { Id = 3, Title = "Deploy app", IsComplete = false }
};

// GET all todos - TypedResults auto-generates OpenAPI docs!
app.MapGet("/api/todos", () => TypedResults.Ok(todos));

// GET single todo by ID - TypedResults for type safety
app.MapGet("/api/todos/{id}", Results<Ok<Todo>, NotFound> (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    return todo is not null 
        ? TypedResults.Ok(todo) 
        : TypedResults.NotFound();
});

// GET completed todos only
app.MapGet("/api/todos/completed", () => 
{
    return TypedResults.Ok(todos.Where(t => t.IsComplete));
});

// Count todos
app.MapGet("/api/todos/count", () => 
{
    return TypedResults.Ok(new { Total = todos.Count, Completed = todos.Count(t => t.IsComplete) });
});

app.Run();

// Try these URLs:
// http://localhost:5000/api/todos
// http://localhost:5000/api/todos/1
// http://localhost:5000/api/todos/completed
// http://localhost:5000/api/todos/count
// http://localhost:5000/openapi/v1.json (auto-generated docs!)
```
