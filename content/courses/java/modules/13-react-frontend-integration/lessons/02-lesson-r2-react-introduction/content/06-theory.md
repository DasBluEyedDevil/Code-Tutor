---
type: "THEORY"
title: "Conditional Rendering"
---

Show different UI based on conditions:

IF/ELSE WITH TERNARY:

function Greeting({ isLoggedIn, username }) {
    return (
        <div>
            {isLoggedIn 
                ? <h1>Welcome back, {username}!</h1>
                : <h1>Please sign in</h1>
            }
        </div>
    );
}

SHORT-CIRCUIT WITH &&:

function Notifications({ count }) {
    return (
        <div>
            <h1>Dashboard</h1>
            {count > 0 && (
                <span className="badge">{count} new messages</span>
            )}
        </div>
    );
}
// If count is 0, nothing renders after Dashboard

EARLY RETURN:

function UserProfile({ user, isLoading }) {
    if (isLoading) {
        return <div>Loading...</div>;
    }
    
    if (!user) {
        return <div>User not found</div>;
    }
    
    return (
        <div>
            <h1>{user.name}</h1>
            <p>{user.email}</p>
        </div>
    );
}

RENDERING LISTS:

function TodoList({ todos }) {
    return (
        <ul>
            {todos.map(todo => (
                <li key={todo.id} className={todo.done ? 'completed' : ''}>
                    {todo.text}
                </li>
            ))}
        </ul>
    );
}