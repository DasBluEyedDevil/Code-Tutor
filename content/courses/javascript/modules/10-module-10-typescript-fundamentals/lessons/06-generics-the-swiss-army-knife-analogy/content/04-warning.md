---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes with generics:

1. **Using `any` instead of generics**:
   ```typescript
   // Bad - loses type safety
   function getFirst(arr: any[]): any {
     return arr[0];
   }
   
   // Good - preserves type safety
   function getFirst<T>(arr: T[]): T | undefined {
     return arr[0];
   }
   ```

2. **Forgetting type constraints**:
   ```typescript
   // Error - T might not have 'length'
   function logLength<T>(item: T): void {
     console.log(item.length);  // ERROR!
   }
   
   // Fixed - constrain T to have length
   function logLength<T extends { length: number }>(item: T): void {
     console.log(item.length);  // OK
   }
   ```

3. **Over-complicating with generics**:
   - Don't use generics when a simple union type works
   - `string | number` might be clearer than `T`
   - Use generics when you need type relationships

4. **Confusing generic syntax**:
   - `<T>` goes after function name: `function name<T>(...)`
   - `<T>` goes after interface name: `interface Name<T> {...}`
   - Arrow functions: `const fn = <T>(x: T): T => x;`

5. **Not specifying return types**:
   ```typescript
   // TypeScript infers, but explicit is clearer
   function getFirst<T>(arr: T[]): T | undefined {
     return arr[0];
   }
   ```

6. **Naming conventions**:
   - T, U, V for general types
   - K for keys, V for values
   - E for elements in collections
   - More descriptive names also work: `TItem`, `TResponse`