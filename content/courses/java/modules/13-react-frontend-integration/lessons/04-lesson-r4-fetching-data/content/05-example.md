---
type: "EXAMPLE"
title: "Complete CRUD Example"
---

Full example with Create, Read, Update, Delete operations:

```jsx
import { useState, useEffect } from 'react';

const API_URL = 'http://localhost:8080/api/todos';

function TodoApp() {
    const [todos, setTodos] = useState([]);
    const [newTodo, setNewTodo] = useState('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    
    // READ - Fetch all todos
    useEffect(() => {
        fetchTodos();
    }, []);
    
    async function fetchTodos() {
        try {
            setLoading(true);
            const response = await fetch(API_URL);
            if (!response.ok) throw new Error('Failed to fetch todos');
            const data = await response.json();
            setTodos(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    }
    
    // CREATE - Add new todo
    async function handleAdd(e) {
        e.preventDefault();
        if (!newTodo.trim()) return;
        
        try {
            const response = await fetch(API_URL, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ text: newTodo, completed: false })
            });
            if (!response.ok) throw new Error('Failed to create todo');
            const created = await response.json();
            setTodos([...todos, created]);
            setNewTodo('');
        } catch (err) {
            setError(err.message);
        }
    }
    
    // UPDATE - Toggle completion
    async function handleToggle(todo) {
        try {
            const response = await fetch(`${API_URL}/${todo.id}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ ...todo, completed: !todo.completed })
            });
            if (!response.ok) throw new Error('Failed to update todo');
            const updated = await response.json();
            setTodos(todos.map(t => t.id === updated.id ? updated : t));
        } catch (err) {
            setError(err.message);
        }
    }
    
    // DELETE - Remove todo
    async function handleDelete(id) {
        try {
            const response = await fetch(`${API_URL}/${id}`, {
                method: 'DELETE'
            });
            if (!response.ok) throw new Error('Failed to delete todo');
            setTodos(todos.filter(t => t.id !== id));
        } catch (err) {
            setError(err.message);
        }
    }
    
    if (loading) return <div>Loading todos...</div>;
    
    return (
        <div className="todo-app">
            <h1>Todo List</h1>
            
            {error && (
                <div className="error">
                    {error}
                    <button onClick={() => setError(null)}>Dismiss</button>
                </div>
            )}
            
            <form onSubmit={handleAdd}>
                <input
                    value={newTodo}
                    onChange={(e) => setNewTodo(e.target.value)}
                    placeholder="Add new todo..."
                />
                <button type="submit">Add</button>
            </form>
            
            <ul>
                {todos.map(todo => (
                    <li key={todo.id} className={todo.completed ? 'completed' : ''}>
                        <input
                            type="checkbox"
                            checked={todo.completed}
                            onChange={() => handleToggle(todo)}
                        />
                        <span>{todo.text}</span>
                        <button onClick={() => handleDelete(todo.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
```
