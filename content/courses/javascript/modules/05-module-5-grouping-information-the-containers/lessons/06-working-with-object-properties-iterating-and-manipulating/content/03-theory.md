---
type: "THEORY"
title: "Mastering the Object"
---

Because objects are not ordered, we can't use a regular `for` loop with `i = 0`. Instead, we use tools that convert the object into something easier to work with.

### 1. The `Object` Utility
The `Object` keyword (with a capital O) provides several helper functions:
*   **`Object.keys(obj)`:** Returns an Array of all property names.
*   **`Object.values(obj)`:** Returns an Array of all property values.
*   **`Object.entries(obj)`:** Returns an Array of Arrays, where each inner array is `[key, value]`. This is perfect for when you want to use array methods (like `map` or `filter`) on an object.

### 2. The `for...in` Loop
This loop is designed specifically to visit every "enumerable" property in an object.
```javascript
for (const key in myObject) {
    // key is the label
    // myObject[key] is the value
}
```
**Important:** `for...in` should be used for Objects. `for...of` should be used for Arrays. Don't mix them up!

### 3. Modifying Properties
*   **Adding/Updating:** Just assign a value: `obj.newKey = 'value'`.
*   **Removing:** Use the `delete` keyword: `delete obj.keyToRemove`. This completely removes the key from the object, so that `'keyToRemove' in obj` becomes `false`.

### 4. Counting Properties
To find out how many properties an object has, use `Object.keys(obj).length`.
