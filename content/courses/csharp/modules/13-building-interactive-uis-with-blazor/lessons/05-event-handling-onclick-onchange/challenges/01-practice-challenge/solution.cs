Console.WriteLine(@"
// TaskManager.razor
using System;
using System.Collections.Generic;
using System.Linq;

<div class=""task-manager"">
    <h3>📝 Task Manager</h3>
    
    <div class=""input-group mb-3"">
        <input 
            class=""form-control""
            @bind=""currentTaskName""
            @onkeydown=""HandleKeyPress""
            placeholder=""Enter task name"" />
        <button class=""btn btn-primary"" @onclick=""AddTask"">Add Task</button>
    </div>
    
    <div class=""alert alert-info"">
        Preview: @(string.IsNullOrEmpty(currentTaskName) ? ""(empty)"" : currentTaskName)
    </div>
    
    <ul class=""list-group"">
        @foreach (var task in tasks)
        {
            <li class=""list-group-item"">
                <input 
                    type=""checkbox"" 
                    checked=""@task.IsCompleted""
                    @onchange=""() => ToggleTask(task.Id)"" />
                <span class=""@(task.IsCompleted ? \""text-decoration-line-through\"" : """")"">
                    @task.Name
                </span>
                <button 
                    class=""btn btn-sm btn-danger float-end"" 
                    @onclick=""() => DeleteTask(task.Id)"">Delete</button>
            </li>
        }
    </ul>
    
    <div class=""mt-3"">
        <strong>Total: @tasks.Count</strong> | 
        <strong>Completed: @tasks.Count(t => t.IsCompleted)</strong> |
        <strong>Remaining: @tasks.Count(t => !t.IsCompleted)</strong>
    </div>
</div>

@code {
    private class TaskItem {
        public int Id { get; set; }
        public string Name { get; set; } = """";
        public bool IsCompleted { get; set; }
    }
    
    private List<TaskItem> tasks = new();
    private string currentTaskName = """";
    private int nextId = 1;
    
    private void AddTask() {
        if (!string.IsNullOrWhiteSpace(currentTaskName)) {
            tasks.Add(new TaskItem { 
                Id = nextId++, 
                Name = currentTaskName, 
                IsCompleted = false 
            });
            currentTaskName = """";
            Console.WriteLine($""Task added! Total: {tasks.Count}"");
        }
    }
    
    private void HandleKeyPress(KeyboardEventArgs e) {
        if (e.Key == ""Enter"") {
            AddTask();
        }
    }
    
    private void ToggleTask(int taskId) {
        var task = tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null) {
            task.IsCompleted = !task.IsCompleted;
            Console.WriteLine($""{task.Name}: {(task.IsCompleted ? \""Completed\"" : \""Incomplete\"")}"");
        }
    }
    
    private void DeleteTask(int taskId) {
        var task = tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null) {
            tasks.Remove(task);
            Console.WriteLine($""Deleted: {task.Name}"");
        }
    }
}
"");

Console.WriteLine(@"
=== EVENTS DEMONSTRATED ===");
Console.WriteLine("✓ @bind - Two-way binding for input");
Console.WriteLine("✓ @onkeydown - Enter key to add task");
Console.WriteLine("✓ @onclick - Add and Delete buttons");
Console.WriteLine("✓ @onchange - Checkbox toggle");
Console.WriteLine("✓ Lambda with parameters: () => DeleteTask(id)");
Console.WriteLine("\n✓ All events → C# methods → UI updates!");