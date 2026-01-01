---
type: "THEORY"
title: "Interface vs. Type Alias"
---

This is one of the most common questions in TypeScript. Both can describe an object, but they have different strengths.

### 1. Interfaces
*   **The Contract:** Interfaces are strictly for objects and classes. 
*   **Extensible:** Interfaces can be "opened" and changed later (Interface Merging). If you define `interface User` twice, TypeScript will combine them into one. 
*   **Recommendation:** Use interfaces for defining the structure of public-facing objects and libraries.

### 2. Type Aliases
*   **The Label:** Types can describe anything (strings, numbers, objects, unions). 
*   **Fixed:** Types cannot be changed once defined.
*   **Unions:** Types are the only way to create union types (e.g., `type ID = string | number`).
*   **Recommendation:** Use type aliases for complex logic, unions, or simple primitive aliases.

### 3. Optional Properties (`?`)
Sometimes data isn't required. By adding a `?` after the property name (`bio?: string`), you tell TypeScript that the property might be `undefined`.

### 4. Readonly Properties
Adding `readonly` before a property prevents it from being reassigned after the object is first created. This is perfect for IDs, creation dates, or settings that shouldn't be touched.

### 5. Duck Typing (Structural Typing)
TypeScript uses "Duck Typing." If an object **walks like a duck and quacks like a duck**, it's a duck. 
If a function expects a `User` interface, and you pass it a plain object that has all the correct properties, TypeScript will accept it—even if you never explicitly said the object "is" a `User`.
