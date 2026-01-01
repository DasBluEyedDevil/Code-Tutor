---
type: "THEORY"
title: "The Logic of Generics"
---

Generics allow you to create reusable components that work with a variety of types rather than a single one.

### 1. The Type Variable `<T>`
The `<T>` syntax is a placeholder for a type. You can think of it like a parameter for types. While `T` is the standard convention (short for Type), you can use any name (like `<Item>` or `<Value>`).

### 2. Type Inference in Generics
You often don't need to write `identity<string>("Hello")`. TypeScript sees the "Hello" and automatically "plugs in" `string` for `T`. This makes generics feel invisible and seamless.

### 3. Constraints (`extends`)
Sometimes you need `T` to be flexible, but not *too* flexible. By using `T extends SomeType`, you are setting a boundary. 
*   Example: `T extends object` means `T` can be any object, but not a number or a string.

### 4. Generics vs `any`
This is the most important distinction:
*   `any`: "I don't care what this is. Forget about safety."
*   `Generic`: "I don't know what this is *yet*, but I need to make sure the input and output types match exactly."
If you pass a string to a function that uses `any`, you might get a number back. If you pass a string to a function that uses `T`, you are guaranteed to get a string back (if that's how you defined the return type).

### 5. Multi-Type Generics
You can have more than one placeholder:
`function pair<T, U>(first: T, second: U): [T, U] { ... }`
This allows you to capture the relationships between multiple pieces of data.
