---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Let's break down custom types in TypeScript:

1. **Interface Definition**: `interface User { ... }`
   - Starts with the `interface` keyword
   - Name should be PascalCase (capitalized)
   - Properties inside curly braces
   - Each property has a type

2. **Using an Interface**: `let user: User = { ... }`
   - Use the interface name as a type annotation
   - Object must have ALL required properties
   - Properties must match the specified types

3. **Optional Properties**: `description?: string`
   - Question mark `?` after property name
   - Means this property might not exist
   - Still type-safe when it does exist

4. **Type Aliases**: `type Point = { x: number; y: number }`
   - Alternative to interfaces
   - Use the `type` keyword
   - Can create complex types

5. **Union Types**: `type Status = 'pending' | 'approved' | 'rejected'`
   - Pipe `|` means "OR"
   - Value must be one of the listed options
   - Great for status codes, modes, etc.

6. **Method Signatures**: `add(a: number, b: number): number`
   - Functions inside interfaces
   - Parameters have types
   - Return type after the closing parenthesis

When to use Interface vs Type:
- Use `interface` for object shapes (most common)
- Use `type` for unions, primitives, or complex combinations
- Both work for objects, choose based on preference