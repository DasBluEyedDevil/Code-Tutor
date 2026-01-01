---
type: "EXAMPLE"
title: "Common State Patterns"
---

Different types of state and how to update them:

```jsx
import { useState } from 'react';

function StateExamples() {
    // Primitive state
    const [count, setCount] = useState(0);
    const [name, setName] = useState('');
    const [isOpen, setIsOpen] = useState(false);
    
    // Object state - must spread to update!
    const [user, setUser] = useState({ name: '', email: '' });
    
    const updateEmail = (newEmail) => {
        // WRONG: mutates state directly
        // user.email = newEmail;
        
        // RIGHT: create new object
        setUser({ ...user, email: newEmail });
        // or
        setUser(prev => ({ ...prev, email: newEmail }));
    };
    
    // Array state - must create new array!
    const [items, setItems] = useState([]);
    
    const addItem = (item) => {
        // WRONG: mutates state
        // items.push(item);
        
        // RIGHT: create new array
        setItems([...items, item]);
    };
    
    const removeItem = (id) => {
        setItems(items.filter(item => item.id !== id));
    };
    
    const updateItem = (id, newText) => {
        setItems(items.map(item => 
            item.id === id ? { ...item, text: newText } : item
        ));
    };
    
    // Toggle boolean
    const toggleOpen = () => setIsOpen(prev => !prev);
    
    // Increment with previous value
    const increment = () => setCount(prev => prev + 1);
    // Use callback form when new state depends on old state
    
    return (
        <div>
            <button onClick={increment}>Count: {count}</button>
            <button onClick={toggleOpen}>
                {isOpen ? 'Close' : 'Open'}
            </button>
        </div>
    );
}
```
