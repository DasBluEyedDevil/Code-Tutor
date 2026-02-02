---
type: "WARNING"
title: "Iteration Pitfalls"
---

### 1. `for...in` on Arrays
Technically, you *can* use `for...in` on an array, but you **should not**. It will loop through the indices as strings (`"0"`, `"1"`, `"2"`), and it can sometimes pick up extra properties that aren't part of the data. 
*   **Rule:** Use `for...of` for arrays. Use `for...in` or `Object.keys` for objects.

### 2. Order of Properties
While modern JavaScript engines usually keep properties in the order they were added (with numbers first), you should never strictly rely on the order of keys in an object. If the order of your data is critical, use an **Array** instead.

### 3. Deleting vs. Setting to `null`
*   `user.name = null;` — The key "name" still exists, but its value is empty.
*   `delete user.name;` — The key "name" is gone entirely.
In most cases, `delete` is slower than setting a value to `null`, but it’s more "correct" if you truly want the property removed.

### 4. Inherited Properties
The `for...in` loop can sometimes find properties that were "inherited" from the object's parent. To be safe, many developers use `Object.keys()` + `for...of` instead of `for...in` to ensure they only see the object's "own" properties.
