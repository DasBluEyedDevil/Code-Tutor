---
type: "EXAMPLE"
title: "Type Aliases vs Interfaces"
---

Both define custom types, but with key differences. Here's when to use each:

```typescript
// INTERFACES - Best for object shapes and extension
interface User {
  id: number;
  name: string;
}

// Interfaces can be extended
interface Admin extends User {
  permissions: string[];
}

// Declaration merging - interfaces can be reopened!
interface User {
  email: string;  // Added to original User interface
}

const user: User = {
  id: 1,
  name: 'Alice',
  email: 'alice@example.com'  // Required due to merge
};

// TYPE ALIASES - Best for unions, primitives, tuples
type ID = string | number;  // Can't do this with interface

type Point = [number, number];  // Tuple - interface can't do this

type Callback = (data: string) => void;  // Function type

type Status = 'pending' | 'active' | 'closed';  // Literal union

// Type aliases can use intersection (similar to extends)
type AdminUser = User & {
  permissions: string[];
};

// WHEN TO USE WHICH:

// Use INTERFACE when:
// - Defining object shapes (classes, objects)
// - You want declaration merging (library types)
// - Extending other types with 'extends'
interface Component {
  render(): void;
}

// Use TYPE when:
// - Creating union types
// - Creating tuple types
// - Creating mapped or conditional types
// - Aliasing primitives or functions
type EventHandler = (event: Event) => void;
type Nullable<T> = T | null;
type Keys = keyof User;  // 'id' | 'name' | 'email'

// Both work for most object types - pick one and be consistent!
type ConfigType = { debug: boolean; port: number };
interface ConfigInterface { debug: boolean; port: number }
// These are functionally equivalent for basic object shapes
```
