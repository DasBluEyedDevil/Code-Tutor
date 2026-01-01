---
type: "THEORY"
title: "💻 Step 6: Create the Frontend (HTML)"
---

```java
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Task Manager</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
    <div class="container">
        <h1>My Tasks</h1>
        
        <!-- Create Task Form -->
        <div class="create-task">
            <h2>Create New Task</h2>
            <form id="createTaskForm">
                <input type="text" id="title" placeholder="Task title" required>
                <textarea id="description" placeholder="Description"></textarea>
                <button type="submit">Add Task</button>
            </form>
        </div>
        
        <!-- Error/Success Messages -->
        <div id="message" class="message" style="display: none;"></div>
        
        <!-- Task List -->
        <div class="task-list">
            <h2>Tasks</h2>
            <div id="tasks">
                <!-- Tasks will be loaded here by JavaScript -->
            </div>
        </div>
    </div>
    
    <script src="app.js"></script>
</body>
</html>
```