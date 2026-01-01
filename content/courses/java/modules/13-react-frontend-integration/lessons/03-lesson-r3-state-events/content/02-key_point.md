---
type: "KEY_POINT"
title: "useState: The State Hook"
---

useState creates state that persists and triggers re-renders:

import { useState } from 'react';

function Counter() {
    const [count, setCount] = useState(0);
    //     ↑        ↑              ↑
    //  current  setter        initial
    //  value    function      value
    
    function handleClick() {
        setCount(count + 1);  // Updates state AND re-renders
    }
    
    return <button onClick={handleClick}>Count: {count}</button>;
    // Now UI updates!
}

RULES OF useState:

1. Call at TOP of component (not in loops/conditions)
// WRONG
if (someCondition) {
    const [value, setValue] = useState(0);  // Never do this!
}

// RIGHT
const [value, setValue] = useState(0);
if (someCondition) {
    // use value here
}

2. State is COMPONENT-LOCAL
<Counter />  // Has its own count
<Counter />  // Has its own count (independent)

3. Setting state REPLACES the value (immutable)
setCount(5);  // count becomes 5, not count + 5