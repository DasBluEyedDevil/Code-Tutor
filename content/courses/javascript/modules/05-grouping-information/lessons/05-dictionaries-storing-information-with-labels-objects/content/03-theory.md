---
type: "THEORY"
title: "Object Fundamentals"
---

Objects are the heart of JavaScript. Almost everything in the language is either an object or behaves like one.

### 1. Keys and Values
*   **Keys (Properties):** These are always strings (or symbols). Even if you don't put quotes around them (like `name:`), JavaScript treats them as strings.
*   **Values:** These can be any data type—numbers, strings, arrays, other objects, or even functions (which we then call "methods").

### 2. Dot Notation vs. Bracket Notation
*   **Dot Notation (`user.name`):** Use this 99% of the time. It is clean and readable.
*   **Bracket Notation (`user['name']`):** Use this when the property name contains special characters (like a space: `user['first name']`) or when the key is stored in a variable:
    ```javascript
    let key = "age";
    console.log(person[key]); 
    ```

### 3. Shorthand Property Names
In modern JavaScript, if your variable name is the same as your key name, you can use a shorthand:
```javascript
const name = "Alice";
const user = { name }; // Instead of { name: name }
```

### 4. Property Existence
If you try to access a property that doesn't exist, JavaScript returns `undefined`. It doesn't crash!
```javascript
const car = { brand: 'Tesla' };
console.log(car.color); // undefined
```
You can also use the `in` operator to check: `'brand' in car` would be `true`.
