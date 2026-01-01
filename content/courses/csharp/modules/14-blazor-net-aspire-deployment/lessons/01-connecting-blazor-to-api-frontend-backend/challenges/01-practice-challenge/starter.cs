// API - Program.cs
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var todos = new List<Todo> {
    new Todo { Id = 1, Title = "Learn Blazor", IsCompleted = false }
};

app.MapGet("/api/todos", () => todos);

// Add other endpoints...

app.Run();

// Blazor - TodoList.razor
@inject HttpClient Http

<h3>Todo List</h3>

@if (todos == null)
{
    <p>Loading...</p>
}
else
{
    <ul>
        @foreach (var todo in todos)
        {
            <li>
                <input type="checkbox" @bind="todo.IsCompleted" />
                @todo.Title
                <button @onclick="() => DeleteTodo(todo.Id)">Delete</button>
            </li>
        }
    </ul>
}

@code {
    private List<Todo>? todos;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadTodos();
    }
    
    private async Task LoadTodos()
    {
        // Call API
    }
}