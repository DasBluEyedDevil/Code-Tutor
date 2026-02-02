---
type: "THEORY"
title: "The Iterator Pattern"
---

The `for...of` loop was introduced in modern JavaScript (ES6) to make working with collections simpler and more expressive.

### 1. The Syntax
```javascript
for (const item of collection) {
    // code block
}
```
*   **`item`:** A variable name you choose. It will hold the value of the current item in the loop. We usually use `const` here because we aren't changing the variable itself, just assigning it a new value in each "lap."
*   **`collection`:** The list (Array, String, etc.) you want to go through.

### 2. No Index Management
The biggest advantage of `for...of` is that you don't have to deal with `i`. 
*   No `let i = 0;`
*   No `i < list.length;`
*   No `i++`
This removes the possibility of "Off-by-One" errors and Infinite Loops.

### 3. Iterables
The `for...of` loop works on anything that is **iterable**. This includes:
*   **Arrays:** Lists of any type of data.
*   **Strings:** The loop will go through every character in the text.
*   **Maps & Sets:** Advanced collections we will learn later.

### 4. What it CAN'T do
The `for...of` loop is perfect for reading every item, but it doesn't give you the **index** (the position) of the item. If you need to know "This is item #3," you might still need a traditional `for` loop.
