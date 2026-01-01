---
type: "EXAMPLE"
title: "Props: Passing Data to Components"
---

Props (properties) let you pass data from parent to child components:

```jsx
// UserCard component receives props
function UserCard({ name, email, avatar }) {
    return (
        <div className="user-card">
            <img src={avatar} alt={name} />
            <h3>{name}</h3>
            <p>{email}</p>
        </div>
    );
}

// Parent component passes props
function UserList() {
    return (
        <div className="user-list">
            <UserCard 
                name="Alice Johnson" 
                email="alice@example.com"
                avatar="/avatars/alice.jpg"
            />
            <UserCard 
                name="Bob Smith" 
                email="bob@example.com"
                avatar="/avatars/bob.jpg"
            />
        </div>
    );
}

// Rendering a list from data
function UserListFromData({ users }) {
    return (
        <div className="user-list">
            {users.map(user => (
                <UserCard 
                    key={user.id}  // Required for lists!
                    name={user.name}
                    email={user.email}
                    avatar={user.avatar}
                />
            ))}
        </div>
    );
}

// Usage
const users = [
    { id: 1, name: "Alice", email: "alice@example.com", avatar: "/a.jpg" },
    { id: 2, name: "Bob", email: "bob@example.com", avatar: "/b.jpg" }
];

<UserListFromData users={users} />
```
