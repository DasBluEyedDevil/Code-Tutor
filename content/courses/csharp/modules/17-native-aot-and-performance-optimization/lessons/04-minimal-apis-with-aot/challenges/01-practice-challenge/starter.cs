using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

// TODO: Configure HTTP JSON options with source generator

var app = builder.Build();

// In-memory storage (simulated database)
var todos = new List<TodoItem>
{
    new(1, "Learn AOT", false, DateTime.Now.AddDays(1)),
    new(2, "Build Minimal API", false, null)
};
var nextId = 3;

// TODO: Implement endpoints
// GET /todos - returns TodoList with all items

// GET /todos/{id} - returns item or 404

// POST /todos - create new item from CreateTodoRequest

// PUT /todos/{id}/complete - mark complete

// DELETE /todos/{id} - remove item

app.Run();

// TODO: Define models (TodoItem, CreateTodoRequest, TodoList)

// TODO: Create JsonSerializerContext with all types

Console.WriteLine("Todo API ready!");