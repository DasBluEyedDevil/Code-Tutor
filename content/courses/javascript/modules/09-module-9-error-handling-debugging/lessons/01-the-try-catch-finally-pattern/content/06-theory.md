---
type: "THEORY"
title: "The Error Lifecycle"
---

Error handling is about controlling the flow of your program when the unexpected happens.

### 1. `try`
The `try` block contains the code that could potentially throw an error. As soon as an error occurs, JavaScript **immediately stops** running the rest of the `try` block and jumps to the `catch` block.

### 2. `catch (error)`
The `catch` block runs **only if** an error occurred in the `try` block. It gives you access to an "Error Object" which usually has:
*   `.name`: The type of error (e.g., `ReferenceError`).
*   `.message`: A human-readable description of what went wrong.
*   `.stack`: A history of which functions were running when the error happened.

### 3. `throw`
You can use the `throw` keyword to manually trigger an error. You can throw anything (a string, a number), but it is best practice to throw an `Error` object:
`throw new Error("Something went wrong");`

### 4. `finally`
The `finally` block **always** runs, regardless of whether there was an error or not. It even runs if you `return` a value from inside the `try` or `catch` blocks! This makes it the perfect place for "Cleanup" code, like closing database connections or hiding "Loading..." messages.

### 5. Optional Catch Binding
In modern JavaScript (ES2019+), if you don't need to look at the error object, you can simply write `catch { ... }` instead of `catch (e) { ... }`.
