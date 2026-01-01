using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

// Configure JSON with source generator for AOT
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, TodoJsonContext.Default);
});

var app = builder.Build();

// In-memory storage (simulated database)
var todos = new List<TodoItem>
{
    new(1, "Learn AOT", false, DateTime.Now.AddDays(1)),
    new(2, "Build Minimal API", false, null)
};
var nextId = 3;

// GET /todos - List all todos
app.MapGet("/todos", () =>
{
    var result = new TodoList(todos.ToList(), todos.Count);
    return Results.Ok(result);
});

// GET /todos/{id} - Get single todo
app.MapGet("/todos/{id}", (int id) =>
{
    var todo = todos.FirstOrDefault(t => t.Id == id);
    return todo is null 
        ? Results.NotFound() 
        : Results.Ok(todo);
});

// POST /todos - Create new todo
app.MapPost("/todos", (CreateTodoRequest request) =>
{
    var newTodo = new TodoItem(nextId++, request.Title, false, request.DueDate);
    todos.Add(newTodo);
    return Results.Created($"/todos/{newTodo.Id}", newTodo);
});

// PUT /todos/{id}/complete - Mark as complete
app.MapPut("/todos/{id}/complete", (int id) =>
{
    var index = todos.FindIndex(t => t.Id == id);
    if (index < 0)
        return Results.NotFound();
    
    var updated = todos[index] with { IsComplete = true };
    todos[index] = updated;
    return Results.Ok(updated);
});

// DELETE /todos/{id} - Remove todo
app.MapDelete("/todos/{id}", (int id) =>
{
    var removed = todos.RemoveAll(t => t.Id == id);
    return removed > 0 
        ? Results.NoContent() 
        : Results.NotFound();
});

Console.WriteLine("Todo API ready!");
Console.WriteLine("Endpoints:");
Console.WriteLine("  GET    /todos");
Console.WriteLine("  GET    /todos/{id}");
Console.WriteLine("  POST   /todos");
Console.WriteLine("  PUT    /todos/{id}/complete");
Console.WriteLine("  DELETE /todos/{id}");

app.Run();

// Models
public record TodoItem(int Id, string Title, bool IsComplete, DateTime? DueDate);
public record CreateTodoRequest(string Title, DateTime? DueDate);
public record TodoList(List<TodoItem> Items, int TotalCount);

// JSON Source Generator for AOT
[JsonSerializable(typeof(TodoItem))]
[JsonSerializable(typeof(List<TodoItem>))]
[JsonSerializable(typeof(CreateTodoRequest))]
[JsonSerializable(typeof(TodoList))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class TodoJsonContext : JsonSerializerContext { }