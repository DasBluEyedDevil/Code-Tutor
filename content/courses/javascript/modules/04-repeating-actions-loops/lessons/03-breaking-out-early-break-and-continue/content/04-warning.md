---
type: "WARNING"
title: "Control Flow Pitfalls"
---

### 1. The `continue` While Loop Trap
This is a very common bug. If you use `continue` in a `while` loop, you might skip the line that updates your variable!
```javascript
let i = 0;
while (i < 5) {
    if (i === 2) continue; // It jumps back to the top!
    console.log(i);
    i++; // This line never runs if i is 2!
}
```
*   **Result:** Infinite Loop. `i` stays 2 forever.

### 2. "Spaghetti" Logic
If you use too many `break` and `continue` statements, your code becomes very hard to follow. It’s like a book that tells you to "Skip to page 50" every few paragraphs. 
*   **Rule:** If you find yourself using many of these, consider if you can simplify your loop condition instead.

### 3. Code After `break` or `continue`
Any code placed inside the loop *after* a `break` or `continue` will be unreachable. JavaScript will ignore it, and some code editors will even dim the text to show you it's "dead code."
```javascript
if (true) {
    break;
    console.log("This will never run.");
}
```
