---
type: "WARNING"
title: "Interface Pitfalls"
---

### 1. Excess Property Check
If you create a literal object and assign it to an interface, TypeScript is extra strict.
```typescript
interface User { name: string }
const u: User = { name: "Alice", age: 30 }; // ERROR! 'age' is not in User.
```
*   **Why:** TypeScript is trying to prevent you from making typos or adding junk data that won't be used.

### 2. Mandatory vs Optional
If you forget to add the `?` to a property, but then leave that property out of your object, TypeScript will throw an error. Double-check your "Contracts" to make sure only truly required data is marked as mandatory.

### 3. Interface Merging Confusion
Because interfaces can be merged, you might find that an object has properties you didn't expect if another file in your project used the same interface name. This is why many developers prefer **Type Aliases** for internal project logic to avoid accidental merging.

### 4. Semantic Naming
Don't name your interfaces `IUser` or `TUser`. In modern TypeScript, it’s standard practice to just use the name itself: `User`. The code editor will tell you if it's an interface or a type.
