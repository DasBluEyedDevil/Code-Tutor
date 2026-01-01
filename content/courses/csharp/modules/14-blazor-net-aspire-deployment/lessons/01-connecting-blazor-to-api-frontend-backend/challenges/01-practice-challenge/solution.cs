Console.WriteLine(@"
=== API (BACKEND) - Program.cs ===");
Console.WriteLine(@"
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Enable CORS for Blazor
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

class Todo {
    public int Id { get; set; }
    public string Title { get; set; } = """";
    public bool IsCompleted { get; set; }
}

var todos = new List<Todo> {
    new Todo { Id = 1, Title = ""Learn Blazor"", IsCompleted = false },
    new Todo { Id = 2, Title = ""Build API"", IsCompleted = true }
};
int nextId = 3;

app.MapGet(""/api/todos"", () => todos);

app.MapGet(""/api/todos/{id}"", (int id) => {
    var todo = todos.FirstOrDefault(t => t.Id == id);
    return todo is not null ? Results.Ok(todo) : Results.NotFound();
});

app.MapPost(""/api/todos"", (Todo todo) => {
    todo.Id = nextId++;
    todos.Add(todo);
    return Results.Created($""/api/todos/{todo.Id}"", todo);
});

app.MapPut(""/api/todos/{id}"", (int id, Todo updated) => {
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo is null) return Results.NotFound();
    
    todo.Title = updated.Title;
    todo.IsCompleted = updated.IsCompleted;
    return Results.Ok(todo);
});

app.MapDelete(""/api/todos/{id}"", (int id) => {
    var todo = todos.FirstOrDefault(t => t.Id == id);
    if (todo is null) return Results.NotFound();
    
    todos.Remove(todo);
    return Results.NoContent();
});

app.Run();
"");

Console.WriteLine(@"
=== BLAZOR (FRONTEND) - TodoList.razor ===");
Console.WriteLine(@"
@inject HttpClient Http

<div class=""container"">
    <h3>📝 Full-Stack Todo App</h3>
    
    @if (isLoading)
    {
        <div class=""spinner-border"" role=""status""></div>
        <span>Loading...</span>
    }
    else if (errorMessage != null)
    {
        <div class=""alert alert-danger"">@errorMessage</div>
    }
    else
    {
        <div class=""mb-3"">
            <input @bind=""newTodoTitle"" class=""form-control"" placeholder=""New todo..."" />
            <button class=""btn btn-primary"" @onclick=""AddTodo"">Add</button>
        </div>
        
        <ul class=""list-group"">
            @foreach (var todo in todos)
            {
                <li class=""list-group-item"">
                    <input 
                        type=""checkbox"" 
                        checked=""@todo.IsCompleted"
                        @onchange=""() => ToggleTodo(todo)"" />
                    <span class=""@(todo.IsCompleted ? \""text-decoration-line-through\"" : """")"">
                        @todo.Title
                    </span>
                    <button 
                        class=""btn btn-sm btn-danger float-end"" 
                        @onclick=""() => DeleteTodo(todo.Id)"">Delete</button>
                </li>
            }
        </ul>
    }
</div>

@code {
    private class Todo {
        public int Id { get; set; }
        public string Title { get; set; } = """";
        public bool IsCompleted { get; set; }
    }
    
    private List<Todo> todos = new();
    private string newTodoTitle = """";
    private bool isLoading = true;
    private string? errorMessage;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadTodos();
    }
    
    private async Task LoadTodos()
    {
        try
        {
            isLoading = true;
            todos = await Http.GetFromJsonAsync<List<Todo>>(""https://localhost:5001/api/todos"") ?? new();
            errorMessage = null;
        }
        catch (HttpRequestException ex)
        {
            errorMessage = $""API Error: {ex.Message}"";
        }
        finally
        {
            isLoading = false;
        }
    }
    
    private async Task AddTodo()
    {
        if (string.IsNullOrWhiteSpace(newTodoTitle)) return;
        
        var newTodo = new Todo { Title = newTodoTitle, IsCompleted = false };
        var response = await Http.PostAsJsonAsync(""https://localhost:5001/api/todos"", newTodo);
        
        if (response.IsSuccessStatusCode)
        {
            newTodoTitle = """";
            await LoadTodos();
        }
    }
    
    private async Task ToggleTodo(Todo todo)
    {
        todo.IsCompleted = !todo.IsCompleted;
        await Http.PutAsJsonAsync($""https://localhost:5001/api/todos/{todo.Id}"", todo);
    }
    
    private async Task DeleteTodo(int id)
    {
        var response = await Http.DeleteAsync($""https://localhost:5001/api/todos/{id}"");
        if (response.IsSuccessStatusCode)
        {
            await LoadTodos();
        }
    }
}
"");

Console.WriteLine(@"
=== FULL-STACK ARCHITECTURE ===");
Console.WriteLine("Frontend (Blazor) → HttpClient → API → Database");
Console.WriteLine("✓ Blazor: UI, user interaction");
Console.WriteLine("✓ HttpClient: Communication layer");
Console.WriteLine("✓ API: Business logic, data access");
Console.WriteLine("✓ Separation of concerns!");
Console.WriteLine("\nYou're now a FULL-STACK developer!");