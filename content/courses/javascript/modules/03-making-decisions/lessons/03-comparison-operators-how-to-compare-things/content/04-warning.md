---
type: "WARNING"
title: "Comparison Slip-ups"
---

### 1. `=` vs `===` (The Classic)
*   `=` is for **Assigning** (putting a value in a box).
*   `===` is for **Comparing** (asking if two things are the same).
If you use `=` in an `if` statement, you are accidentally changing your data instead of checking it!

### 2. The Case Sensitivity Trap
Beginners often forget that `'User'` is not the same as `'user'`.
```javascript
let status = 'Active';
if (status === 'active') { // FALSE!
   console.log("Welcome!");
}
```
*   **Solution:** Make sure your comparison strings match the data exactly, or convert both to lowercase before checking.

### 3. Comparing Objects or Arrays
Wait! Comparison operators only work reliably on simple types (Numbers, Strings, Booleans). If you try to compare two arrays or two objects using `===`, it will almost always return `false`, even if they look identical. 
*(We will learn why this happens when we get to Objects and References later in the course.)*
