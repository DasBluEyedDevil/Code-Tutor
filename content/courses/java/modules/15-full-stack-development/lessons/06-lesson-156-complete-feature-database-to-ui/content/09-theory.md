---
type: "THEORY"
title: "💻 Step 7: Create the Frontend (JavaScript)"
---

```java
const API_URL = 'http://localhost:8080/api/tasks';

// Load tasks when page loads
document.addEventListener('DOMContentLoaded', () => {
    loadTasks();
    
    // Handle form submission
    document.getElementById('createTaskForm')
        .addEventListener('submit', createTask);
});

// Fetch and display all tasks
async function loadTasks() {
    try {
        const response = await fetch(API_URL);
        if (!response.ok) throw new Error('Failed to load tasks');
        
        const tasks = await response.json();
        displayTasks(tasks);
    } catch (error) {
        showMessage('Error loading tasks: ' + error.message, 'error');
    }
}

// Display tasks in the UI
function displayTasks(tasks) {
    const container = document.getElementById('tasks');
    
    if (tasks.length === 0) {
        container.innerHTML = '<p>No tasks yet. Create one above!</p>';
        return;
    }
    
    container.innerHTML = tasks.map(task => `
        <div class="task ${task.completed ? 'completed' : ''}">
            <div class="task-content">
                <h3>${escapeHtml(task.title)}</h3>
                <p>${escapeHtml(task.description || '')}</p>
            </div>
            <div class="task-actions">
                <button onclick="toggleComplete(${task.id}, ${!task.completed})">
                    ${task.completed ? '✓ Completed' : 'Mark Complete'}
                </button>
                <button onclick="deleteTask(${task.id})" class="delete-btn">
                    Delete
                </button>
            </div>
        </div>
    `).join('');
}

// Create new task
async function createTask(event) {
    event.preventDefault();
    
    const title = document.getElementById('title').value;
    const description = document.getElementById('description').value;
    
    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title, description })
        });
        
        if (!response.ok) throw new Error('Failed to create task');
        
        // Clear form
        document.getElementById('createTaskForm').reset();
        
        // Reload tasks
        loadTasks();
        showMessage('Task created successfully!', 'success');
    } catch (error) {
        showMessage('Error: ' + error.message, 'error');
    }
}

// Toggle task completion
async function toggleComplete(taskId, completed) {
    try {
        const response = await fetch(`${API_URL}/${taskId}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ completed })
        });
        
        if (!response.ok) throw new Error('Failed to update task');
        
        loadTasks();
        showMessage('Task updated!', 'success');
    } catch (error) {
        showMessage('Error: ' + error.message, 'error');
    }
}

// Delete task
async function deleteTask(taskId) {
    if (!confirm('Delete this task?')) return;
    
    try {
        const response = await fetch(`${API_URL}/${taskId}`, {
            method: 'DELETE'
        });
        
        if (!response.ok) throw new Error('Failed to delete task');
        
        loadTasks();
        showMessage('Task deleted!', 'success');
    } catch (error) {
        showMessage('Error: ' + error.message, 'error');
    }
}

// Show message to user
function showMessage(text, type) {
    const msg = document.getElementById('message');
    msg.textContent = text;
    msg.className = `message ${type}`;
    msg.style.display = 'block';
    setTimeout(() => msg.style.display = 'none', 3000);
}

// Prevent XSS attacks
function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}
```