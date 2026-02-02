---
type: "WARNING"
title: "Object Gotchas"
---

### 1. `const` doesn't mean "unmutable"
Just like arrays, if you declare an object with `const`, you can still change its properties.
```javascript
const user = { name: 'Bob' };
user.name = 'Alice'; // LEGAL
user = { name: 'Charlie' }; // ERROR!
```
If you want to make an object truly unchangeable, you have to use a special command called `Object.freeze(user)`.

### 2. Missing Property Errors
While accessing a missing property gives `undefined`, trying to access a property **of** that undefined value will crash your app.
```javascript
const user = {};
console.log(user.address); // undefined
console.log(user.address.city); // CRASH! "Cannot read property 'city' of undefined"
```
*   **Fix:** Always ensure the parent object exists before digging deeper.

### 3. Reserved Words as Keys
While modern JavaScript allows you to use words like `for` or `if` as keys (e.g., `{ for: 'something' }`), it’s generally a bad idea and can lead to confusion.

### 4. Naming with Spaces
If you name a key with a space, like `first name`, you **must** use quotes and you **must** use bracket notation to access it.
```javascript
const user = { "first name": "Alice" };
console.log(user["first name"]); // works
console.log(user.first name);    // SYNTAX ERROR
```
*   **Best Practice:** Always use `camelCase` for your keys (`firstName`).
