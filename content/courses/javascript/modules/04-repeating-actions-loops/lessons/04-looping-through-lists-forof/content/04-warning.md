---
type: "WARNING"
title: "for...of Limitations"
---

### 1. The Object Trap
The `for...of` loop does **not** work on standard JavaScript Objects (containers like `{ name: 'Alice', age: 25 }`). 
If you try to use it on an object, you will get a `TypeError: object is not iterable`.
*   *(Note: There is a different loop called `for...in` for objects, but we'll cover that later!)*

### 2. Missing the Index
As mentioned in the theory section, you don't have access to the position of the item. 
```javascript
const colors = ['red', 'green', 'blue'];
for (const color of colors) {
    // I know the color is 'red', but I don't know it's at index 0!
}
```
If your logic requires knowing the position, stick to the traditional `for (let i = 0; ...)` loop.

### 3. Modifying the List
It is generally a bad idea to add or remove items from the list *while* you are looping through it using `for...of`. This can lead to skipped items or strange behavior. 
*   **Best Practice:** If you need to filter a list, create a **new** empty list and push the items you want to keep into it.
