---
type: "WARNING"
title: "Event Pitfalls"
---

### 1. Calling the function immediately
A common mistake is putting parentheses `()` after your function name in the listener.
*   **Wrong:** `btn.addEventListener('click', myFunc());` (This runs the function **immediately** when the page loads, not when the button is clicked).
*   **Right:** `btn.addEventListener('click', myFunc);`

### 2. Forgetting `preventDefault()`
If you're handling a form with JavaScript but forget `e.preventDefault()`, your page will flash and refresh as soon as you click submit. Any data you saved in JavaScript variables will be **deleted** because the page reloaded.

### 3. Multiple Listeners
If you call `addEventListener` 10 times on the same button, it will run 10 separate functions every time you click it. Be careful not to accidentally set up duplicate listeners inside a loop or a function that runs multiple times.

### 4. Memory Leaks
If you create a listener on an element and then delete that element from the page, some older browsers might keep the listener in memory, slowing down your computer. In modern apps, if you're done with a listener, you can use `removeEventListener`.
