---
type: "THEORY"
title: "The Event Loop"
---

Events are the heart of interactivity. They are signals sent by the browser to tell your code that "something happened."

### 1. `addEventListener(type, callback)`
This is the modern way to listen for events. 
*   **type:** A string representing the event name (e.g., `'click'`, `'submit'`, `'keydown'`, `'mouseover'`).
*   **callback:** The function that should run when the event happens.

### 2. The Event Object (`e`)
When the browser calls your function, it automatically passes in an **Event Object**. This object contains a wealth of information:
*   `e.target`: The element that was actually clicked/typed in.
*   `e.key`: Which key was pressed (for keyboard events).
*   `e.clientX / e.clientY`: The coordinates of the mouse.

### 3. `preventDefault()`
Many HTML elements have "default behaviors." 
*   Clicking a link refreshes the page or moves to a new URL.
*   Submitting a form refreshes the page.
Calling `e.preventDefault()` inside your listener tells the browser: "I'll handle this with JavaScript, don't do your normal routine."

### 4. Common Events
*   **Mouse:** `click`, `dblclick`, `mouseenter`, `mouseleave`.
*   **Keyboard:** `keydown`, `keyup`, `keypress`.
*   **Form:** `submit`, `change`, `input`, `focus`, `blur`.
*   **Window:** `resize`, `scroll`, `load`.
