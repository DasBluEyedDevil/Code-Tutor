---
type: "THEORY"
title: "The Problem: Components Need Memory"
---

Props are great for passing data DOWN. But what about data that CHANGES over time?

function Counter() {
    let count = 0;  // Local variable
    
    function handleClick() {
        count++;  // This changes count...
        console.log(count);  // Logs 1, 2, 3...
    }
    
    return <button onClick={handleClick}>Count: {count}</button>;
    // But UI always shows 0!
}

PROBLEMS:
1. Local variables don't persist between renders
2. Changing local variables doesn't trigger re-render

React re-renders when:
- Props change (parent provides new values)
- State changes (component's own data changes)

Local variables are RESET every render and DON'T trigger re-renders.

SOLUTION: useState hook - gives components MEMORY that persists and triggers updates.