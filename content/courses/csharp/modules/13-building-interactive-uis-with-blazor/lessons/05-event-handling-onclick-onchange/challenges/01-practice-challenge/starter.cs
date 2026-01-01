// TaskManager.razor
<div class="task-manager">
    <h3>Task Manager</h3>
    
    <div class="input-group">
        <input 
            @oninput="HandleTaskInput" 
            @onkeydown="HandleKeyPress"
            placeholder="Enter task name" />
        <button @onclick="AddTask">Add Task</button>
    </div>
    
    <p>Preview: @currentTaskName</p>
    
    <ul>
        @foreach (var task in tasks)
        {
            <li>
                <input 
                    type="checkbox" 
                    checked="@task.IsCompleted"
                    @onchange="() => ToggleTask(task.Id)" />
                <span class="@(task.IsCompleted ? \"completed\" : \"\")">@task.Name</span>
                <button @onclick="() => DeleteTask(task.Id)">Delete</button>
            </li>
        }
    </ul>
    
    <p>Total: @tasks.Count | Completed: @tasks.Count(t => t.IsCompleted)</p>
</div>

@code {
    // Implement task management
}