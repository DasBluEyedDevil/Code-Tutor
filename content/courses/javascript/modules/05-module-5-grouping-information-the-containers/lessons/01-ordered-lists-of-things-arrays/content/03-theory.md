---
type: "THEORY"
title: "The Anatomy of an Array"
---

Arrays are the primary way to manage collections of data in JavaScript.

### 1. The Index (Zero-Based)
The most important rule in arrays is **Zero-Based Indexing**.
*   The 1st item is at `[0]`
*   The 2nd item is at `[1]`
*   The `nth` item is at `[n - 1]`

### 2. The `.length` Property
Every array has a built-in property called `.length` that tells you how many items are currently inside it. 
*   **Empty Array:** `[].length` is `0`.
*   **Updating Length:** If you add items, `.length` updates automatically.

### 3. Arrays are Objects (Wait, what?)
In JavaScript, arrays are a special type of "Object." This is why they have properties (like `.length`) and methods (actions they can perform).

### 4. Constants and Arrays
Notice that we usually use `const` to declare arrays: `const items = [1, 2, 3]`.
*   Even though it's a `const`, you **can** change the items inside (e.g., `items[0] = 99`).
*   The `const` only prevents you from reassining the whole variable to a different array (e.g., `items = [4, 5, 6]` would throw an error).

### 5. Heterogeneous Collections
JavaScript arrays can hold any combination of types—numbers, strings, booleans, or even other arrays! While technically possible, it is best practice to keep arrays "homogeneous" (all items of the same type) to avoid confusion.
