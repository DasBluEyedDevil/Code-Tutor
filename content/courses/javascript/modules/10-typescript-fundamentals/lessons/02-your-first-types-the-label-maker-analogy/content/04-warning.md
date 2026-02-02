---
type: "WARNING"
title: "Type Pitfalls"
---

### 1. `number` vs `Number` (Case Matters)
Always use lowercase names for types (`string`, `number`, `boolean`). 
*   **Why:** Lowercase names refer to the actual primitive type. Uppercase names (`String`, `Number`) refer to built-in JavaScript wrapper objects, which behave differently and should almost never be used as type labels.

### 2. Over-Annotation
Don't feel the need to label everything!
*   **Wrong:** `const name: string = "Alice";` (TypeScript already knows it's a string).
*   **Right:** `const name = "Alice";` (Keep your code clean).
Only add the label when you declare a variable without a value, or when you are defining function parameters.

### 3. Mixed Arrays
If you have an array with both strings and numbers, TypeScript will infer it as `(string | number)[]`. Be careful with these, as you'll need to check the type of each item before using it.

### 4. The `any` temptation
If you're stuck, it's tempting to use `any` to make the error go away. 
*   **Rule:** If you use `any`, you are effectively turning off TypeScript for that variable. Use it only as a last resort during migration from JavaScript.
