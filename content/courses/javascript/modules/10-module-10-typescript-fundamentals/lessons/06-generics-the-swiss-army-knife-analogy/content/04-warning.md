---
type: "WARNING"
title: "Generic Pitfalls"
---

### 1. Over-Engineering
Don't use generics if you don't need them.
*   **Wrong:** `function getName<T extends { name: string }>(item: T): string { ... }`
*   **Right:** `function getName(item: { name: string }): string { ... }`
If your function only ever works with one specific shape of data, a generic is just making your code harder to read.

### 2. Missing Constraints
If you use a generic `T` and then try to use a property like `T.name`, TypeScript will complain.
*   **Why:** TypeScript doesn't know if `T` is an object, a number, or a boolean. 
*   **Fix:** Use `extends` to tell TypeScript that `T` is an object with a `name` property.

### 3. Naming Confusion
While `<T>` is standard, if you have a complex function with many generics, using names like `<UserType>`, `<ResponseType>`, and `<ErrorType>` makes your code much easier for other developers to understand.

### 4. Nested Generics
Be careful when nesting generics (e.g., `Map<string, List<T>>`). The syntax can quickly become a "triangle of death" that is hard to debug. If it gets too complex, consider breaking it down into smaller, named interfaces.
