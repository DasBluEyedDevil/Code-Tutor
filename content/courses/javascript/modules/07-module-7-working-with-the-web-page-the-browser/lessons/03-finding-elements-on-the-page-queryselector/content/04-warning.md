---
type: "WARNING"
title: "Selection Pitfalls"
---

### 1. Forgetting the Symbols
This is the most common mistake. In CSS, IDs need a `#` and classes need a `.`.
*   **Wrong:** `document.querySelector('my-id')`
*   **Right:** `document.querySelector('#my-id')`
If you forget the symbol, JavaScript will look for a tag called `<my-id>`, which doesn't exist!

### 2. The `null` Crash
If `querySelector` doesn't find anything, it returns `null`. If you immediately try to change that element, your app will crash.
```javascript
const btn = document.querySelector('#missing-button');
btn.textContent = "Click me"; // CRASH! Cannot set property of null.
```
*   **Fix:** Always check if your element exists before using it: `if (btn) { ... }`

### 3. `querySelectorAll` doesn't have properties
You cannot change all elements at once by using the list.
```javascript
const allBtns = document.querySelectorAll('button');
allBtns.style.color = 'red'; // ERROR! The LIST doesn't have a style property.
```
*   **Fix:** You must loop through the list and change each button individually.

### 4. Live vs. Static
`querySelectorAll` returns a **static** list. If you find all buttons, and then you add a new button to the page using code, the new button will **not** be in your old list. You would need to run the query again to find it.
