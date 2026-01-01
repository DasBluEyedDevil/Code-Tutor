---
type: "THEORY"
title: "The Basic Types"
---

The core of TypeScript is assigning types to your variables and function signatures.

### 1. Primitive Types
These map directly to the JavaScript types you already know:
*   `string`: All text data.
*   `number`: All numbers (integers and decimals).
*   `boolean`: `true` or `false`.

### 2. Array Types
You can describe a list of items using the `type[]` syntax. 
*   `string[]` is a list of strings.
*   `number[]` is a list of numbers.
You can also use the generic syntax: `Array<string>`, though the bracket syntax is more common.

### 3. Type Inference
You don't always have to type out the labels. If you initialize a variable when you declare it, TypeScript will "infer" the type.
`let active = true;` — TypeScript knows this is a boolean. You don't need to write `let active: boolean = true;`. 

**Best Practice:** Use inference when the type is obvious, and explicit annotations when it's not.

### 4. Literal Types and Unions
You can restrict a variable to specific exact values.
`let status: "online" | "offline";`
The `|` (pipe) symbol means "OR." This variable can only be one of those two specific strings. This is incredibly powerful for preventing typos in your application logic.

### 5. `null` and `undefined`
By default, these are their own types in TypeScript. In "Strict" mode, you cannot assign `null` to a `string` variable unless you explicitly allow it: `let name: string | null = null;`.
