---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Let's break down generics in TypeScript:

1. **Basic Generic Syntax**: `function name<T>(param: T): T`
   - `<T>` declares a type parameter (T is just a convention)
   - `T` is a placeholder for any type
   - Common names: T (Type), K (Key), V (Value), E (Element)

2. **Using Generic Functions**:
   ```typescript
   // Explicit type argument
   getFirst<string>(['a', 'b']);
   
   // Type inference (TypeScript figures it out)
   getFirst(['a', 'b']);  // Infers string
   ```

3. **Generic Interfaces**: `interface Box<T> { contents: T }`
   - Create reusable shapes that work with any type
   - Specify the type when using: `Box<number>`

4. **Multiple Type Parameters**: `<K, V>`
   - Use multiple placeholders for complex types
   - Each can be different types

5. **Generic Constraints**: `<T extends SomeType>`
   - Limit which types can be used
   - `extends` means "must have these properties"
   - Ensures the generic type has required features

6. **Generic Classes**: `class Container<T> { ... }`
   - Create reusable data structures
   - Type is specified when creating instances
   - Methods work with the specified type

7. **When to Use Generics**:
   - Functions that work with multiple types
   - Data structures (lists, stacks, maps)
   - API wrappers and response types
   - Utility functions (getFirst, filter, map)

8. **Benefits of Generics**:
   - Write once, use with many types
   - Full type safety maintained
   - Better code reuse
   - Self-documenting code