---
type: "KEY_POINT"
title: "useEffect: Side Effects in React"
---

Components shouldn't fetch data during render. Use useEffect:

import { useState, useEffect } from 'react';

function UserList() {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    
    useEffect(() => {
        // This runs AFTER render
        async function fetchUsers() {
            try {
                const response = await fetch('/api/users');
                if (!response.ok) throw new Error('Failed to fetch');
                const data = await response.json();
                setUsers(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        }
        
        fetchUsers();
    }, []);  // Empty array = run once on mount
    
    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;
    
    return (
        <ul>
            {users.map(user => <li key={user.id}>{user.name}</li>)}
        </ul>
    );
}

DEPENDENCY ARRAY:
useEffect(() => { }, []);         // Run once on mount
useEffect(() => { }, [userId]);   // Run when userId changes
useEffect(() => { });             // Run on every render (usually wrong!)